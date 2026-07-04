namespace Hospital_Managment_system.Models;

/// <summary>
/// Represents an appointment between a patient and doctor.
/// </summary>
public class Appointment : BaseEntity
{
    /// <summary>
    /// Patient ID.
    /// </summary>
    public int PatientId { get; set; }

    /// <summary>
    /// Doctor ID.
    /// </summary>
    public int DoctorId { get; set; }

    /// <summary>
    /// Date and time of the appointment.
    /// </summary>
    public DateTime AppointmentDateTime { get; set; }

    /// <summary>
    /// Reason for the appointment.
    /// </summary>
    public string? ReasonForVisit { get; set; }

    /// <summary>
    /// Status of the appointment (e.g., Scheduled, Completed, Cancelled, Rescheduled).
    /// </summary>
    public required string Status { get; set; } = "Scheduled";

    /// <summary>
    /// Notes from the doctor about the appointment.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Appointment type (e.g., Consultation, Follow-up).
    /// </summary>
    public string? AppointmentType { get; set; } = "Consultation";

    /// <summary>
    /// Duration of the appointment in minutes.
    /// </summary>
    public int DurationInMinutes { get; set; } = 30;

    // Navigation properties
    public Patient? Patient { get; set; }
    public Doctor? Doctor { get; set; }
    public MedicalRecord? MedicalRecord { get; set; }
}
