using Hospital_Managment_system.DTOs;

namespace Hospital_Managment_system.Interfaces;

/// <summary>
/// Service interface for department operations.
/// </summary>
public interface IDepartmentService
{
    /// <summary>
    /// Create a new department.
    /// </summary>
    Task<DepartmentDetailDto> CreateDepartmentAsync(DepartmentDto departmentDto);

    /// <summary>
    /// Get department by ID.
    /// </summary>
    Task<DepartmentDetailDto?> GetDepartmentAsync(int departmentId);

    /// <summary>
    /// Get all departments with pagination.
    /// </summary>
    Task<PaginatedListDto<DepartmentDetailDto>> GetAllDepartmentsAsync(int pageNumber, int pageSize);

    /// <summary>
    /// Update department.
    /// </summary>
    Task<DepartmentDetailDto> UpdateDepartmentAsync(int departmentId, DepartmentDto departmentDto);

    /// <summary>
    /// Delete department.
    /// </summary>
    Task<bool> DeleteDepartmentAsync(int departmentId);

    /// <summary>
    /// Search departments.
    /// </summary>
    Task<IList<DepartmentDetailDto>> SearchDepartmentsAsync(string searchTerm);

    /// <summary>
    /// Get department with doctors.
    /// </summary>
    Task<DepartmentDetailDto?> GetDepartmentWithDoctorsAsync(int departmentId);
}
