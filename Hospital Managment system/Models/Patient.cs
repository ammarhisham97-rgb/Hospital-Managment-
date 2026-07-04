namespace Hospital_Managment_system.Models;

/// <summary>
/// Represents a patient in the hospital.
/// </summary>
public class Patient : BaseEntity
{
    /// <summary>
    /// User ID associated with this patient.
    /// </summary>
    public required string UserId { get; set; }

    /// <summary>
    /// Patient ID number (unique identifier for patient records).
    /// </summary>
    public required string PatientNumber { get; set; }

    /// <summary>
    /// Blood group of the patient (e.g., O+, B-).
    /// </summary>
    public string? BloodGroup { get; set; }

    /// <summary>
    /// Allergies of the patient.
    /// </summary>
    public string? Allergies { get; set; }

    /// <summary>
    /// Medical history of the patient.
    /// </summary>
    public string? MedicalHistory { get; set; }

    /// <summary>
    /// Emergency contact name.
    /// </summary>
    public string? EmergencyContactName { get; set; }

    /// <summary>
    /// Emergency contact phone number.
    /// </summary>
    public string? EmergencyContactPhone { get; set; }

    /// <summary>
    /// Insurance provider name.
    /// </summary>
    public string? InsuranceProvider { get; set; }

    /// <summary>
    /// Insurance policy number.
    /// </summary>
    public string? InsurancePolicyNumber { get; set; }

    /// <summary>
    /// Flag indicating if the patient is active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    // Navigation properties
    public User? User { get; set; }
    public ICollection<Appointment> Appointments { get; set; } = [];
    public ICollection<MedicalRecord> MedicalRecords { get; set; } = [];
}
