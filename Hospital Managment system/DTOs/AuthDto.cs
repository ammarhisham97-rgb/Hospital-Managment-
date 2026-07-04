namespace Hospital_Managment_system.DTOs;

/// <summary>
/// DTO for user registration requests.
/// </summary>
public class RegisterDto
{
    /// <summary>
    /// User's full name.
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// User's email address.
    /// </summary>
    public required string Email { get; set; }

    /// <summary>
    /// User's phone number.
    /// </summary>
    public required string PhoneNumber { get; set; }

    /// <summary>
    /// User's password.
    /// </summary>
    public required string Password { get; set; }

    /// <summary>
    /// Password confirmation.
    /// </summary>
    public required string ConfirmPassword { get; set; }

    /// <summary>
    /// User's date of birth.
    /// </summary>
    public DateTime? DateOfBirth { get; set; }

    /// <summary>
    /// User's address.
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// User's city.
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    /// User's state/province.
    /// </summary>
    public string? State { get; set; }

    /// <summary>
    /// User's postal code.
    /// </summary>
    public string? PostalCode { get; set; }

    /// <summary>
    /// User's country.
    /// </summary>
    public string? Country { get; set; }

    /// <summary>
    /// Role to assign to the user (Patient, Doctor, Admin).
    /// </summary>
    public required string Role { get; set; } = "Patient";
}

/// <summary>
/// DTO for user login requests.
/// </summary>
public class LoginDto
{
    /// <summary>
    /// User's email address.
    /// </summary>
    public required string Email { get; set; }

    /// <summary>
    /// User's password.
    /// </summary>
    public required string Password { get; set; }
}

/// <summary>
/// DTO for refresh token requests.
/// </summary>
public class RefreshTokenDto
{
    /// <summary>
    /// The refresh token.
    /// </summary>
    public required string RefreshToken { get; set; }
}

/// <summary>
/// DTO for authentication responses.
/// </summary>
public class AuthResponseDto
{
    /// <summary>
    /// User ID.
    /// </summary>
    public required string UserId { get; set; }

    /// <summary>
    /// User's email.
    /// </summary>
    public required string Email { get; set; }

    /// <summary>
    /// User's full name.
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// JWT access token.
    /// </summary>
    public required string AccessToken { get; set; }

    /// <summary>
    /// Refresh token.
    /// </summary>
    public required string RefreshToken { get; set; }

    /// <summary>
    /// Token expiration time in seconds.
    /// </summary>
    public int ExpiresIn { get; set; }

    /// <summary>
    /// User's roles.
    /// </summary>
    public IList<string> Roles { get; set; } = new List<string>();
}

/// <summary>
/// DTO for change password requests.
/// </summary>
public class ChangePasswordDto
{
    /// <summary>
    /// Current password.
    /// </summary>
    public required string CurrentPassword { get; set; }

    /// <summary>
    /// New password.
    /// </summary>
    public required string NewPassword { get; set; }

    /// <summary>
    /// Confirm new password.
    /// </summary>
    public required string ConfirmPassword { get; set; }
}

/// <summary>
/// DTO for API responses.
/// </summary>
/// <typeparam name="T">Type of the data being returned.</typeparam>
public class ApiResponseDto<T>
{
    /// <summary>
    /// Indicates if the request was successful.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Message describing the result.
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// The returned data.
    /// </summary>
    public T? Data { get; set; }

    /// <summary>
    /// Error details (if any).
    /// </summary>
    public string? Error { get; set; }

    /// <summary>
    /// HTTP status code.
    /// </summary>
    public int StatusCode { get; set; }
}

/// <summary>
/// DTO for pagination metadata.
/// </summary>
public class PaginationDto
{
    /// <summary>
    /// Current page number.
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// Number of items per page.
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Total number of items.
    /// </summary>
    public int TotalItems { get; set; }

    /// <summary>
    /// Total number of pages.
    /// </summary>
    public int TotalPages { get; set; }
}

/// <summary>
/// DTO for paginated list responses.
/// </summary>
/// <typeparam name="T">Type of items in the list.</typeparam>
public class PaginatedListDto<T>
{
    /// <summary>
    /// List of items.
    /// </summary>
    public IList<T> Items { get; set; } = new List<T>();

    /// <summary>
    /// Pagination metadata.
    /// </summary>
    public PaginationDto? Pagination { get; set; }
}
