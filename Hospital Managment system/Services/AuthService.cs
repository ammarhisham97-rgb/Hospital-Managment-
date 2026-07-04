using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Hospital_Managment_system.DTOs;
using Hospital_Managment_system.Interfaces;
using Hospital_Managment_system.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace Hospital_Managment_system.Services;

/// <summary>
/// Service for authentication operations.
/// </summary>
public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public AuthService(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IConfiguration configuration,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
    {
        // Check if user already exists
        var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);
        if (existingUser != null)
        {
            Log.Warning("Registration attempted with existing email: {Email}", registerDto.Email);
            throw new InvalidOperationException("Email is already registered");
        }

        // Create user
        var user = new User
        {
            UserName = registerDto.Email,
            Email = registerDto.Email,
            FullName = registerDto.FullName,
            PhoneNumber = registerDto.PhoneNumber,
            DateOfBirth = registerDto.DateOfBirth,
            Address = registerDto.Address,
            City = registerDto.City,
            State = registerDto.State,
            PostalCode = registerDto.PostalCode,
            Country = registerDto.Country,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            Log.Warning("User registration failed: {Errors}", errors);
            throw new InvalidOperationException($"Registration failed: {errors}");
        }

        // Assign role
        await _userManager.AddToRoleAsync(user, registerDto.Role);

        // Create patient record if role is Patient
        if (registerDto.Role == "Patient")
        {
            var patient = new Patient
            {
                UserId = user.Id,
                PatientNumber = GeneratePatientNumber(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            await _unitOfWork.Patients.AddAsync(patient);
            await _unitOfWork.SaveChangesAsync();
        }

        Log.Information("User registered successfully: {Email}", user.Email);

        var tokens = GenerateJwtToken(user, new List<string> { registerDto.Role });
        return new AuthResponseDto
        {
            UserId = user.Id,
            Email = user.Email,
            FullName = user.FullName ?? "",
            AccessToken = tokens.accessToken,
            RefreshToken = tokens.refreshToken,
            ExpiresIn = int.Parse(_configuration["JwtSettings:ExpiryMinutes"] ?? "60") * 60,
            Roles = new List<string> { registerDto.Role }
        };
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user == null || user.IsDeleted)
        {
            Log.Warning("Login attempted with invalid email: {Email}", loginDto.Email);
            throw new InvalidOperationException("Invalid email or password");
        }

        var result = await _signInManager.PasswordSignInAsync(user.UserName!, loginDto.Password, false, false);
        if (!result.Succeeded)
        {
            Log.Warning("Login failed for user: {Email}", loginDto.Email);
            throw new InvalidOperationException("Invalid email or password");
        }

        var roles = await _userManager.GetRolesAsync(user);
        var tokens = GenerateJwtToken(user, roles.ToList());

        Log.Information("User logged in successfully: {Email}", user.Email);

        return new AuthResponseDto
        {
            UserId = user.Id,
            Email = user.Email,
            FullName = user.FullName ?? "",
            AccessToken = tokens.accessToken,
            RefreshToken = tokens.refreshToken,
            ExpiresIn = int.Parse(_configuration["JwtSettings:ExpiryMinutes"] ?? "60") * 60,
            Roles = roles
        };
    }

    public Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenDto refreshTokenDto)
    {
        // Note: In a production system, you would validate and decode the refresh token
        // from a secure store (database). This is a simplified implementation.
        throw new NotImplementedException("Refresh token functionality should be implemented with a token store");
    }

    public async Task<bool> ChangePasswordAsync(string userId, ChangePasswordDto changePasswordDto)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            Log.Warning("Change password attempted for non-existent user: {UserId}", userId);
            throw new KeyNotFoundException("User not found");
        }

        var result = await _userManager.ChangePasswordAsync(user, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            Log.Warning("Password change failed for user {UserId}: {Errors}", userId, errors);
            throw new InvalidOperationException($"Password change failed: {errors}");
        }

        Log.Information("Password changed successfully for user: {UserId}", userId);
        return true;
    }

    public async Task<AuthResponseDto?> GetUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null || user.IsDeleted)
            return null;

        var roles = await _userManager.GetRolesAsync(user);

        return new AuthResponseDto
        {
            UserId = user.Id,
            Email = user.Email ?? "",
            FullName = user.FullName ?? "",
            AccessToken = "",
            RefreshToken = "",
            ExpiresIn = 0,
            Roles = roles
        };
    }

    private (string accessToken, string refreshToken) GenerateJwtToken(User user, List<string> roles)
    {
        var secretKey = _configuration["JwtSettings:SecretKey"];
        var issuer = _configuration["JwtSettings:Issuer"];
        var audience = _configuration["JwtSettings:Audience"];
        var expiryMinutes = int.Parse(_configuration["JwtSettings:ExpiryMinutes"] ?? "60");

        var key = Encoding.ASCII.GetBytes(secretKey!);
        var tokenHandler = new JwtSecurityTokenHandler();

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Email, user.Email!),
            new(ClaimTypes.Name, user.FullName ?? user.UserName!),
            new("UserId", user.Id)
        };

        // Add role claims
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(expiryMinutes),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var accessToken = tokenHandler.WriteToken(token);

        // Generate a simple refresh token (in production, store this in DB)
        var refreshToken = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{user.Id}|{DateTime.UtcNow.AddDays(7).Ticks}"));

        return (accessToken, refreshToken);
    }

    private static string GeneratePatientNumber()
    {
        return $"PT-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper()}";
    }
}
