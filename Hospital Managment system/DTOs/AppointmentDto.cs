namespace Hospital_Managment_system.DTOs;

/// <summary>
/// DTO for creating or updating an appointment.
/// </summary>
public class AppointmentDto
{
    /// <summary>
    /// Appointment ID.
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    /// Patient ID.
    /// </summary>
    public int PatientId { get; set; }

    /// <summary>
    /// Doctor ID.
    /// </summary>
    public int DoctorId { get; set; }

    /// <summary>
    /// Appointment date and time.
    /// </summary>
    public DateTime AppointmentDateTime { get; set; }

    /// <summary>
    /// Reason for visit.
    /// </summary>
    public string? ReasonForVisit { get; set; }

    /// <summary>
    /// Appointment type.
    /// </summary>
    public string? AppointmentType { get; set; }

    /// <summary>
    /// Duration in minutes.
    /// </summary>
    public int DurationInMinutes { get; set; }
}

/// <summary>
/// DTO for viewing appointment details.
/// </summary>
public class AppointmentDetailDto
{
    /// <summary>
    /// Appointment ID.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Patient ID.
    /// </summary>
    public int PatientId { get; set; }

    /// <summary>
    /// Patient name.
    /// </summary>
    public required string PatientName { get; set; }

    /// <summary>
    /// Doctor ID.
    /// </summary>
    public int DoctorId { get; set; }

    /// <summary>
    /// Doctor name.
    /// </summary>
    public required string DoctorName { get; set; }

    /// <summary>
    /// Appointment date and time.
    /// </summary>
    public DateTime AppointmentDateTime { get; set; }

    /// <summary>
    /// Reason for visit.
    /// </summary>
    public string? ReasonForVisit { get; set; }

    /// <summary>
    /// Status.
    /// </summary>
    public required string Status { get; set; }

    /// <summary>
    /// Notes.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Appointment type.
    /// </summary>
    public string? AppointmentType { get; set; }

    /// <summary>
    /// Duration in minutes.
    /// </summary>
    public int DurationInMinutes { get; set; }

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
/// DTO for rescheduling an appointment.
/// </summary>
public class RescheduleAppointmentDto
{
    /// <summary>
    /// New appointment date and time.
    /// </summary>
    public DateTime NewAppointmentDateTime { get; set; }

    /// <summary>
    /// Reason for rescheduling.
    /// </summary>
    public string? Reason { get; set; }
}

/// <summary>
/// DTO for canceling an appointment.
/// </summary>
public class CancelAppointmentDto
{
    /// <summary>
    /// Reason for cancellation.
    /// </summary>
    public string? Reason { get; set; }
}
