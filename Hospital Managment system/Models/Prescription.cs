namespace Hospital_Managment_system.Models;

/// <summary>
/// Represents a prescription issued by a doctor.
/// </summary>
public class Prescription : BaseEntity
{
    /// <summary>
    /// Medical record ID this prescription belongs to.
    /// </summary>
    public int MedicalRecordId { get; set; }

    /// <summary>
    /// Name of the medication.
    /// </summary>
    public required string MedicationName { get; set; }

    /// <summary>
    /// Dosage prescribed (e.g., 500mg, 10ml).
    /// </summary>
    public required string Dosage { get; set; }

    /// <summary>
    /// Frequency of taking the medication (e.g., Twice daily, Every 8 hours).
    /// </summary>
    public required string Frequency { get; set; }

    /// <summary>
    /// Duration of the medication course (e.g., 1 week, 10 days).
    /// </summary>
    public required string Duration { get; set; }

    /// <summary>
    /// Special instructions for the medication.
    /// </summary>
    public string? Instructions { get; set; }

    /// <summary>
    /// Quantity of the medication prescribed.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Number of refills allowed.
    /// </summary>
    public int Refills { get; set; } = 0;

    // Navigation properties
    public MedicalRecord? MedicalRecord { get; set; }
}
