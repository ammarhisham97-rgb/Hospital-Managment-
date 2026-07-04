using Hospital_Managment_system.DTOs;

namespace Hospital_Managment_system.Interfaces;

/// <summary>
/// Service interface for authentication operations.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Register a new user.
    /// </summary>
    Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);

    /// <summary>
    /// Login user and return JWT token.
    /// </summary>
    Task<AuthResponseDto> LoginAsync(LoginDto loginDto);

    /// <summary>
    /// Refresh access token using refresh token.
    /// </summary>
    Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenDto refreshTokenDto);

    /// <summary>
    /// Change user password.
    /// </summary>
    Task<bool> ChangePasswordAsync(string userId, ChangePasswordDto changePasswordDto);

    /// <summary>
    /// Get user by ID.
    /// </summary>
    Task<AuthResponseDto?> GetUserAsync(string userId);
}
