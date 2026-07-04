namespace Hospital_Managment_system.Models;

/// <summary>
/// Represents a medical record created during or after an appointment.
/// </summary>
public class MedicalRecord : BaseEntity
{
    /// <summary>
    /// Patient ID.
    /// </summary>
    public int PatientId { get; set; }

    /// <summary>
    /// Doctor ID who created the record.
    /// </summary>
    public int DoctorId { get; set; }

    /// <summary>
    /// Appointment ID associated with this record.
    /// </summary>
    public int? AppointmentId { get; set; }

    /// <summary>
    /// Diagnosis information.
    /// </summary>
    public required string Diagnosis { get; set; }

    /// <summary>
    /// Visit notes from the doctor.
    /// </summary>
    public string? VisitNotes { get; set; }

    /// <summary>
    /// Symptoms observed by the doctor.
    /// </summary>
    public string? Symptoms { get; set; }

    /// <summary>
    /// Physical examination findings.
    /// </summary>
    public string? PhysicalExamination { get; set; }

    /// <summary>
    /// Lab test results.
    /// </summary>
    public string? LabTestResults { get; set; }

    /// <summary>
    /// Treatment plan.
    /// </summary>
    public string? TreatmentPlan { get; set; }

    // Navigation properties
    public Patient? Patient { get; set; }
    public Doctor? Doctor { get; set; }
    public Appointment? Appointment { get; set; }
    public ICollection<Prescription> Prescriptions { get; set; } = [];
}
