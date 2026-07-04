namespace Hospital_Managment_system.DTOs;

/// <summary>
/// DTO for creating or updating a medical record.
/// </summary>
public class MedicalRecordDto
{
    /// <summary>
    /// Medical record ID.
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
    /// Appointment ID.
    /// </summary>
    public int? AppointmentId { get; set; }

    /// <summary>
    /// Diagnosis.
    /// </summary>
    public required string Diagnosis { get; set; }

    /// <summary>
    /// Visit notes.
    /// </summary>
    public string? VisitNotes { get; set; }

    /// <summary>
    /// Symptoms.
    /// </summary>
    public string? Symptoms { get; set; }

    /// <summary>
    /// Physical examination.
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
}

/// <summary>
/// DTO for viewing medical record details.
/// </summary>
public class MedicalRecordDetailDto
{
    /// <summary>
    /// Medical record ID.
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
    /// Appointment ID.
    /// </summary>
    public int? AppointmentId { get; set; }

    /// <summary>
    /// Diagnosis.
    /// </summary>
    public required string Diagnosis { get; set; }

    /// <summary>
    /// Visit notes.
    /// </summary>
    public string? VisitNotes { get; set; }

    /// <summary>
    /// Symptoms.
    /// </summary>
    public string? Symptoms { get; set; }

    /// <summary>
    /// Physical examination.
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

    /// <summary>
    /// Prescriptions.
    /// </summary>
    public IList<PrescriptionDto> Prescriptions { get; set; } = new List<PrescriptionDto>();

    /// <summary>
    /// Created date.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Updated date.
    /// </summary>
    public DateTime UpdatedAt { get; set; }
}
