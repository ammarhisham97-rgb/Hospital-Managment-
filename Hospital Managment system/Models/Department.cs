namespace Hospital_Managment_system.Models;

/// <summary>
/// Represents a department in the hospital.
/// </summary>
public class Department : BaseEntity
{
    /// <summary>
    /// Name of the department (e.g., Cardiology, Orthopedics).
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Description of the department.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Contact number for the department.
    /// </summary>
    public string? ContactNumber { get; set; }

    /// <summary>
    /// Email address of the department.
    /// </summary>
    public string? Email { get; set; }

    // Navigation properties
    public ICollection<Doctor> Doctors { get; set; } = [];
}
