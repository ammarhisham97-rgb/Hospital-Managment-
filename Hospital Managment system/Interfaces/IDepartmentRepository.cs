using Hospital_Managment_system.Models;

namespace Hospital_Managment_system.Interfaces;

/// <summary>
/// Specific repository for Department entity.
/// </summary>
public interface IDepartmentRepository : IRepository<Department>
{
    /// <summary>
    /// Get department by name.
    /// </summary>
    Task<Department?> GetByNameAsync(string name);

    /// <summary>
    /// Get department with doctors.
    /// </summary>
    Task<Department?> GetWithDoctorsAsync(int departmentId);

    /// <summary>
    /// Search departments.
    /// </summary>
    Task<IEnumerable<Department>> SearchAsync(string searchTerm);
}
