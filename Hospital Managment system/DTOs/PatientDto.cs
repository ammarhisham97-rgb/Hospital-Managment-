namespace Hospital_Managment_system.DTOs;

/// <summary>
/// DTO for creating or updating a patient.
/// </summary>
public class PatientDto
{
    /// <summary>
    /// Patient ID.
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    /// User ID.
    /// </summary>
    public string? UserId { get; set; }

    /// <summary>
    /// Blood group.
    /// </summary>
    public string? BloodGroup { get; set; }

    /// <summary>
    /// Allergies.
    /// </summary>
    public string? Allergies { get; set; }

    /// <summary>
    /// Medical history.
    /// </summary>
    public string? MedicalHistory { get; set; }

    /// <summary>
    /// Emergency contact name.
    /// </summary>
    public string? EmergencyContactName { get; set; }

    /// <summary>
    /// Emergency contact phone.
    /// </summary>
    public string? EmergencyContactPhone { get; set; }

    /// <summary>
    /// Insurance provider.
    /// </summary>
    public string? InsuranceProvider { get; set; }

    /// <summary>
    /// Insurance policy number.
    /// </summary>
    public string? InsurancePolicyNumber { get; set; }
}

/// <summary>
/// DTO for viewing patient details.
/// </summary>
public class PatientDetailDto
{
    /// <summary>
    /// Patient ID.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// User ID.
    /// </summary>
    public required string UserId { get; set; }

    /// <summary>
    /// Patient number.
    /// </summary>
    public required string PatientNumber { get; set; }

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
    /// Date of birth.
    /// </summary>
    public DateTime? DateOfBirth { get; set; }

    /// <summary>
    /// Blood group.
    /// </summary>
    public string? BloodGroup { get; set; }

    /// <summary>
    /// Allergies.
    /// </summary>
    public string? Allergies { get; set; }

    /// <summary>
    /// Medical history.
    /// </summary>
    public string? MedicalHistory { get; set; }

    /// <summary>
    /// Emergency contact name.
    /// </summary>
    public string? EmergencyContactName { get; set; }

    /// <summary>
    /// Emergency contact phone.
    /// </summary>
    public string? EmergencyContactPhone { get; set; }

    /// <summary>
    /// Insurance provider.
    /// </summary>
    public string? InsuranceProvider { get; set; }

    /// <summary>
    /// Insurance policy number.
    /// </summary>
    public string? InsurancePolicyNumber { get; set; }

    /// <summary>
    /// Is active.
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Address.
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Profile image URL.
    /// </summary>
    public string? ProfileImageUrl { get; set; }

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
/// DTO for patient search results.
/// </summary>
public class PatientSearchDto
{
    /// <summary>
    /// Patient ID.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Patient number.
    /// </summary>
    public required string PatientNumber { get; set; }

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
    /// Blood group.
    /// </summary>
    public string? BloodGroup { get; set; }
}
