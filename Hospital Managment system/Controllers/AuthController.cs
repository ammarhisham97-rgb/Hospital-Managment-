using FluentValidation;
using Hospital_Managment_system.DTOs;
using Hospital_Managment_system.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Managment_system.Controllers;

/// <summary>
/// Controller for authentication endpoints.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IValidator<RegisterDto> _registerValidator;
    private readonly IValidator<LoginDto> _loginValidator;
    private readonly IValidator<ChangePasswordDto> _changePasswordValidator;

    public AuthController(
        IAuthService authService,
        IValidator<RegisterDto> registerValidator,
        IValidator<LoginDto> loginValidator,
        IValidator<ChangePasswordDto> changePasswordValidator)
    {
        _authService = authService;
        _registerValidator = registerValidator;
        _loginValidator = loginValidator;
        _changePasswordValidator = changePasswordValidator;
    }

    /// <summary>
    /// Register a new user.
    /// </summary>
    [HttpPost("register")]
    [ProducesResponseType(typeof(ApiResponseDto<AuthResponseDto>), 200)]
    [ProducesResponseType(typeof(ApiResponseDto<string>), 400)]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var validationResult = await _registerValidator.ValidateAsync(registerDto);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ApiResponseDto<string>
            {
                Success = false,
                Message = "Validation failed",
                Error = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)),
                StatusCode = 400
            });
        }

        try
        {
            var result = await _authService.RegisterAsync(registerDto);
            return Ok(new ApiResponseDto<AuthResponseDto>
            {
                Success = true,
                Message = "Registration successful",
                Data = result,
                StatusCode = 200
            });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new ApiResponseDto<string>
            {
                Success = false,
                Message = "Registration failed",
                Error = ex.Message,
                StatusCode = 400
            });
        }
    }

    /// <summary>
    /// Login user.
    /// </summary>
    [HttpPost("login")]
    [ProducesResponseType(typeof(ApiResponseDto<AuthResponseDto>), 200)]
    [ProducesResponseType(typeof(ApiResponseDto<string>), 401)]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var validationResult = await _loginValidator.ValidateAsync(loginDto);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ApiResponseDto<string>
            {
                Success = false,
                Message = "Validation failed",
                Error = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)),
                StatusCode = 400
            });
        }

        try
        {
            var result = await _authService.LoginAsync(loginDto);
            return Ok(new ApiResponseDto<AuthResponseDto>
            {
                Success = true,
                Message = "Login successful",
                Data = result,
                StatusCode = 200
            });
        }
        catch (InvalidOperationException)
        {
            return Unauthorized(new ApiResponseDto<string>
            {
                Success = false,
                Message = "Login failed",
                Error = "Invalid email or password",
                StatusCode = 401
            });
        }
    }

    /// <summary>
    /// Get current user profile.
    /// </summary>
    [HttpGet("me")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponseDto<AuthResponseDto>), 200)]
    [ProducesResponseType(typeof(ApiResponseDto<string>), 404)]
    public async Task<IActionResult> GetMe()
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new ApiResponseDto<string>
            {
                Success = false,
                Message = "Unauthorized",
                Error = "User ID not found in token",
                StatusCode = 401
            });
        }

        var user = await _authService.GetUserAsync(userId);
        if (user == null)
        {
            return NotFound(new ApiResponseDto<string>
            {
                Success = false,
                Message = "User not found",
                StatusCode = 404
            });
        }

        return Ok(new ApiResponseDto<AuthResponseDto>
        {
            Success = true,
            Message = "User retrieved successfully",
            Data = user,
            StatusCode = 200
        });
    }

    /// <summary>
    /// Change password.
    /// </summary>
    [HttpPost("change-password")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponseDto<string>), 200)]
    [ProducesResponseType(typeof(ApiResponseDto<string>), 400)]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
    {
        var validationResult = await _changePasswordValidator.ValidateAsync(changePasswordDto);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ApiResponseDto<string>
            {
                Success = false,
                Message = "Validation failed",
                Error = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)),
                StatusCode = 400
            });
        }

        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new ApiResponseDto<string>
            {
                Success = false,
                Message = "Unauthorized",
                Error = "User ID not found",
                StatusCode = 401
            });
        }

        try
        {
            await _authService.ChangePasswordAsync(userId, changePasswordDto);
            return Ok(new ApiResponseDto<string>
            {
                Success = true,
                Message = "Password changed successfully",
                StatusCode = 200
            });
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new ApiResponseDto<string>
            {
                Success = false,
                Message = "User not found",
                StatusCode = 404
            });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new ApiResponseDto<string>
            {
                Success = false,
                Message = "Password change failed",
                Error = ex.Message,
                StatusCode = 400
            });
        }
    }
}
