using Microsoft.AspNetCore.Identity;

namespace Hospital_Managment_system.Models;

/// <summary>
/// Represents a user in the system (Admin, Doctor, Receptionist, Patient).
/// Inherits from IdentityUser for authentication and authorization.
/// </summary>
public class User : IdentityUser
{
    /// <summary>
    /// Full name of the user.
    /// </summary>
    public string? FullName { get; set; }

    /// <summary>
    /// Date of birth of the user.
    /// </summary>
    public DateTime? DateOfBirth { get; set; }

    /// <summary>
    /// Profile image URL.
    /// </summary>
    public string? ProfileImageUrl { get; set; }

    /// <summary>
    /// Address of the user.
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// City of the user.
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    /// State/Province of the user.
    /// </summary>
    public string? State { get; set; }

    /// <summary>
    /// Postal code of the user.
    /// </summary>
    public string? PostalCode { get; set; }

    /// <summary>
    /// Country of the user.
    /// </summary>
    public string? Country { get; set; }

    /// <summary>
    /// Date and time when the user account was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Date and time when the user account was last updated.
    /// </summary>
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Flag indicating if the user has been soft deleted.
    /// </summary>
    public bool IsDeleted { get; set; } = false;

    /// <summary>
    /// Date and time when the user was soft deleted.
    /// </summary>
    public DateTime? DeletedAt { get; set; }

    // Navigation properties
    public Patient? Patient { get; set; }
    public Doctor? Doctor { get; set; }
}
