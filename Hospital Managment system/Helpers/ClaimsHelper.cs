using System.Security.Claims;

namespace Hospital_Managment_system.Helpers;

/// <summary>
/// Helper class for JWT token operations.
/// </summary>
public static class ClaimsHelper
{
    /// <summary>
    /// Get the user ID from claims.
    /// </summary>
    public static string? GetUserId(ClaimsPrincipal user)
    {
        return user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }

    /// <summary>
    /// Get the email from claims.
    /// </summary>
    public static string? GetEmail(ClaimsPrincipal user)
    {
        return user?.FindFirst(ClaimTypes.Email)?.Value;
    }

    /// <summary>
    /// Get the user roles from claims.
    /// </summary>
    public static IList<string> GetRoles(ClaimsPrincipal user)
    {
        var roles = user?.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList() ?? new List<string>();
        return roles;
    }

    /// <summary>
    /// Check if user is in a specific role.
    /// </summary>
    public static bool IsInRole(ClaimsPrincipal user, string role)
    {
        return user?.IsInRole(role) ?? false;
    }
}
