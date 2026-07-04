namespace Hospital_Managment_system.DTOs;

/// <summary>
/// DTO for creating or updating a doctor.
/// </summary>
public class DoctorDto
{
    /// <summary>
    /// Doctor ID.
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    /// User ID associated with the doctor.
    /// </summary>
    public string? UserId { get; set; }

    /// <summary>
    /// Medical license number.
    /// </summary>
    public required string LicenseNumber { get; set; }

    /// <summary>
    /// Specialization.
    /// </summary>
    public required string Specialization { get; set; }

    /// <summary>
    /// Years of experience.
    /// </summary>
    public int YearsOfExperience { get; set; }

    /// <summary>
    /// Department ID.
    /// </summary>
    public int DepartmentId { get; set; }

    /// <summary>
    /// Qualifications.
    /// </summary>
    public string? Qualifications { get; set; }

    /// <summary>
    /// Consultation fee.
    /// </summary>
    public decimal ConsultationFee { get; set; }

    /// <summary>
    /// Is the doctor available.
    /// </summary>
    public bool IsAvailable { get; set; }
}

/// <summary>
/// DTO for viewing doctor details.
/// </summary>
public class DoctorDetailDto
{
    /// <summary>
    /// Doctor ID.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// User ID.
    /// </summary>
    public required string UserId { get; set; }

    /// <summary>
    /// Full name.
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Email.
    /// </summary>
    public required string Email { get; set; }

    /// <summary>
    /// Phone number.
    /// </summary>
    public required string PhoneNumber { get; set; }

    /// <summary>
    /// Medical license number.
    /// </summary>
    public required string LicenseNumber { get; set; }

    /// <summary>
    /// Specialization.
    /// </summary>
    public required string Specialization { get; set; }

    /// <summary>
    /// Years of experience.
    /// </summary>
    public int YearsOfExperience { get; set; }

    /// <summary>
    /// Department ID.
    /// </summary>
    public int DepartmentId { get; set; }

    /// <summary>
    /// Department name.
    /// </summary>
    public required string DepartmentName { get; set; }

    /// <summary>
    /// Qualifications.
    /// </summary>
    public string? Qualifications { get; set; }

    /// <summary>
    /// Consultation fee.
    /// </summary>
    public decimal ConsultationFee { get; set; }

    /// <summary>
    /// Is available.
    /// </summary>
    public bool IsAvailable { get; set; }

    /// <summary>
    /// Profile image URL.
    /// </summary>
    public string? ProfileImageUrl { get; set; }

    /// <summary>
    /// Address.
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Created date.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Updated date.
    /// </summary>
    public DateTime UpdatedAt { get; set; }
}

/// <summary>
/// DTO for doctor schedule.
/// </summary>
public class DoctorScheduleDto
{
    /// <summary>
    /// Schedule ID.
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    /// Doctor ID.
    /// </summary>
    public int DoctorId { get; set; }

    /// <summary>
    /// Day of the week (0=Sunday, 6=Saturday).
    /// </summary>
    public int DayOfWeek { get; set; }

    /// <summary>
    /// Start time.
    /// </summary>
    public TimeOnly StartTime { get; set; }

    /// <summary>
    /// End time.
    /// </summary>
    public TimeOnly EndTime { get; set; }

    /// <summary>
    /// Appointment duration in minutes.
    /// </summary>
    public int AppointmentDurationInMinutes { get; set; }

    /// <summary>
    /// Is active.
    /// </summary>
    public bool IsActive { get; set; }
}
