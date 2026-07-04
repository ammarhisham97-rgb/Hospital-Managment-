using Hospital_Managment_system.DTOs;

namespace Hospital_Managment_system.Interfaces;

/// <summary>
/// Service interface for medical record operations.
/// </summary>
public interface IMedicalRecordService
{
    /// <summary>
    /// Create a new medical record.
    /// </summary>
    Task<MedicalRecordDetailDto> CreateMedicalRecordAsync(MedicalRecordDto medicalRecordDto);

    /// <summary>
    /// Get medical record by ID.
    /// </summary>
    Task<MedicalRecordDetailDto?> GetMedicalRecordAsync(int recordId);

    /// <summary>
    /// Get all medical records with pagination.
    /// </summary>
    Task<PaginatedListDto<MedicalRecordDetailDto>> GetAllMedicalRecordsAsync(int pageNumber, int pageSize);

    /// <summary>
    /// Update medical record.
    /// </summary>
    Task<MedicalRecordDetailDto> UpdateMedicalRecordAsync(int recordId, MedicalRecordDto medicalRecordDto);

    /// <summary>
    /// Delete medical record.
    /// </summary>
    Task<bool> DeleteMedicalRecordAsync(int recordId);

    /// <summary>
    /// Get patient medical records.
    /// </summary>
    Task<IList<MedicalRecordDetailDto>> GetPatientMedicalRecordsAsync(int patientId);

    /// <summary>
    /// Get doctor medical records.
    /// </summary>
    Task<IList<MedicalRecordDetailDto>> GetDoctorMedicalRecordsAsync(int doctorId);

    /// <summary>
    /// Add prescription to medical record.
    /// </summary>
    Task<PrescriptionDetailDto> AddPrescriptionAsync(int recordId, PrescriptionDto prescriptionDto);

    /// <summary>
    /// Get prescriptions for medical record.
    /// </summary>
    Task<IList<PrescriptionDetailDto>> GetPrescriptionsAsync(int recordId);

    /// <summary>
    /// Update prescription.
    /// </summary>
    Task<PrescriptionDetailDto> UpdatePrescriptionAsync(int prescriptionId, PrescriptionDto prescriptionDto);

    /// <summary>
    /// Delete prescription.
    /// </summary>
    Task<bool> DeletePrescriptionAsync(int prescriptionId);
}
