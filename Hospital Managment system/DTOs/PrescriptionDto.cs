namespace Hospital_Managment_system.DTOs;

/// <summary>
/// DTO for creating or updating a prescription.
/// </summary>
public class PrescriptionDto
{
    /// <summary>
    /// Prescription ID.
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    /// Medical record ID.
    /// </summary>
    public int MedicalRecordId { get; set; }

    /// <summary>
    /// Medication name.
    /// </summary>
    public required string MedicationName { get; set; }

    /// <summary>
    /// Dosage.
    /// </summary>
    public required string Dosage { get; set; }

    /// <summary>
    /// Frequency.
    /// </summary>
    public required string Frequency { get; set; }

    /// <summary>
    /// Duration.
    /// </summary>
    public required string Duration { get; set; }

    /// <summary>
    /// Instructions.
    /// </summary>
    public string? Instructions { get; set; }

    /// <summary>
    /// Quantity.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Refills.
    /// </summary>
    public int Refills { get; set; }
}

/// <summary>
/// DTO for viewing prescription details.
/// </summary>
public class PrescriptionDetailDto
{
    /// <summary>
    /// Prescription ID.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Medical record ID.
    /// </summary>
    public int MedicalRecordId { get; set; }

    /// <summary>
    /// Medication name.
    /// </summary>
    public required string MedicationName { get; set; }

    /// <summary>
    /// Dosage.
    /// </summary>
    public required string Dosage { get; set; }

    /// <summary>
    /// Frequency.
    /// </summary>
    public required string Frequency { get; set; }

    /// <summary>
    /// Duration.
    /// </summary>
    public required string Duration { get; set; }

    /// <summary>
    /// Instructions.
    /// </summary>
    public string? Instructions { get; set; }

    /// <summary>
    /// Quantity.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Refills.
    /// </summary>
    public int Refills { get; set; }

    /// <summary>
    /// Created date.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Updated date.
    /// </summary>
    public DateTime UpdatedAt { get; set; }
}
