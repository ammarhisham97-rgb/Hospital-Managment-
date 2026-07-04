using Hospital_Managment_system.DTOs;
using Hospital_Managment_system.Models;

namespace Hospital_Managment_system.Interfaces;

/// <summary>
/// Service interface for patient operations.
/// </summary>
public interface IPatientService
{
    /// <summary>
    /// Create a new patient.
    /// </summary>
    Task<PatientDetailDto> CreatePatientAsync(PatientDto patientDto, string userId);

    /// <summary>
    /// Get patient by ID.
    /// </summary>
    Task<PatientDetailDto?> GetPatientAsync(int patientId);

    /// <summary>
    /// Get all patients with pagination.
    /// </summary>
    Task<PaginatedListDto<PatientDetailDto>> GetAllPatientsAsync(int pageNumber, int pageSize);

    /// <summary>
    /// Update patient information.
    /// </summary>
    Task<PatientDetailDto> UpdatePatientAsync(int patientId, PatientDto patientDto);

    /// <summary>
    /// Delete patient.
    /// </summary>
    Task<bool> DeletePatientAsync(int patientId);

    /// <summary>
    /// Search patients.
    /// </summary>
    Task<IList<PatientSearchDto>> SearchPatientsAsync(string searchTerm);

    /// <summary>
    /// Get patient medical history.
    /// </summary>
    Task<IList<MedicalRecordDetailDto>> GetPatientMedicalHistoryAsync(int patientId);
}
