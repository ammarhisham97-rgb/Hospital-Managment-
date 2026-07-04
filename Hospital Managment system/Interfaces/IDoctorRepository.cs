using Hospital_Managment_system.Models;

namespace Hospital_Managment_system.Interfaces;

/// <summary>
/// Specific repository for Doctor entity.
/// </summary>
public interface IDoctorRepository : IRepository<Doctor>
{
    /// <summary>
    /// Get doctor by user ID.
    /// </summary>
    Task<Doctor?> GetByUserIdAsync(string userId);

    /// <summary>
    /// Get doctor by license number.
    /// </summary>
    Task<Doctor?> GetByLicenseNumberAsync(string licenseNumber);

    /// <summary>
    /// Get doctors by department.
    /// </summary>
    Task<IEnumerable<Doctor>> GetByDepartmentAsync(int departmentId);

    /// <summary>
    /// Get doctors by specialization.
    /// </summary>
    Task<IEnumerable<Doctor>> GetBySpecializationAsync(string specialization);

    /// <summary>
    /// Search doctors by name or specialization.
    /// </summary>
    Task<IEnumerable<Doctor>> SearchAsync(string searchTerm);

    /// <summary>
    /// Get available doctors.
    /// </summary>
    Task<IEnumerable<Doctor>> GetAvailableAsync();

    /// <summary>
    /// Get doctor with all related data.
    /// </summary>
    Task<Doctor?> GetWithDetailsAsync(int doctorId);

    /// <summary>
    /// Get doctor with schedule.
    /// </summary>
    Task<Doctor?> GetWithScheduleAsync(int doctorId);
}
