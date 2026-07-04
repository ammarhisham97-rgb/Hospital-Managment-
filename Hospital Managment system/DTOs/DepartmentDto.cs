namespace Hospital_Managment_system.DTOs;

/// <summary>
/// DTO for creating or updating a department.
/// </summary>
public class DepartmentDto
{
    /// <summary>
    /// Department ID.
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    /// Department name.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Department description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Department contact number.
    /// </summary>
    public string? ContactNumber { get; set; }

    /// <summary>
    /// Department email.
    /// </summary>
    public string? Email { get; set; }
}

/// <summary>
/// DTO for viewing a department with its doctors.
/// </summary>
public class DepartmentDetailDto
{
    /// <summary>
    /// Department ID.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Department name.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Department description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Department contact number.
    /// </summary>
    public string? ContactNumber { get; set; }

    /// <summary>
    /// Department email.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Number of doctors in the department.
    /// </summary>
    public int DoctorCount { get; set; }

    /// <summary>
    /// Created date.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Updated date.
    /// </summary>
    public DateTime UpdatedAt { get; set; }
}
