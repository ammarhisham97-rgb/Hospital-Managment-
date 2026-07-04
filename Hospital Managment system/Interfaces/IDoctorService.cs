using Hospital_Managment_system.DTOs;

namespace Hospital_Managment_system.Interfaces;

/// <summary>
/// Service interface for doctor operations.
/// </summary>
public interface IDoctorService
{
    /// <summary>
    /// Create a new doctor.
    /// </summary>
    Task<DoctorDetailDto> CreateDoctorAsync(DoctorDto doctorDto, string userId);

    /// <summary>
    /// Get doctor by ID.
    /// </summary>
    Task<DoctorDetailDto?> GetDoctorAsync(int doctorId);

    /// <summary>
    /// Get all doctors with pagination.
    /// </summary>
    Task<PaginatedListDto<DoctorDetailDto>> GetAllDoctorsAsync(int pageNumber, int pageSize);

    /// <summary>
    /// Update doctor information.
    /// </summary>
    Task<DoctorDetailDto> UpdateDoctorAsync(int doctorId, DoctorDto doctorDto);

    /// <summary>
    /// Delete doctor.
    /// </summary>
    Task<bool> DeleteDoctorAsync(int doctorId);

    /// <summary>
    /// Get doctors by specialization.
    /// </summary>
    Task<IList<DoctorDetailDto>> GetDoctorsBySpecializationAsync(string specialization);

    /// <summary>
    /// Get doctors by department.
    /// </summary>
    Task<IList<DoctorDetailDto>> GetDoctorsByDepartmentAsync(int departmentId);

    /// <summary>
    /// Get available doctors.
    /// </summary>
    Task<IList<DoctorDetailDto>> GetAvailableDoctorsAsync();

    /// <summary>
    /// Search doctors.
    /// </summary>
    Task<IList<DoctorDetailDto>> SearchDoctorsAsync(string searchTerm);

    /// <summary>
    /// Get doctor schedule.
    /// </summary>
    Task<IList<DoctorScheduleDto>> GetDoctorScheduleAsync(int doctorId);

    /// <summary>
    /// Add doctor schedule.
    /// </summary>
    Task<DoctorScheduleDto> AddDoctorScheduleAsync(int doctorId, DoctorScheduleDto scheduleDto);

    /// <summary>
    /// Update doctor schedule.
    /// </summary>
    Task<DoctorScheduleDto> UpdateDoctorScheduleAsync(int scheduleId, DoctorScheduleDto scheduleDto);

    /// <summary>
    /// Delete doctor schedule.
    /// </summary>
    Task<bool> DeleteDoctorScheduleAsync(int scheduleId);
}
