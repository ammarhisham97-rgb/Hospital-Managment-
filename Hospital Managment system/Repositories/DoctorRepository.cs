using Microsoft.EntityFrameworkCore;
using Hospital_Managment_system.Data;
using Hospital_Managment_system.Interfaces;
using Hospital_Managment_system.Models;

namespace Hospital_Managment_system.Repositories;

/// <summary>
/// Repository implementation for Doctor entity.
/// </summary>
public class DoctorRepository : Repository<Doctor>, IDoctorRepository
{
    public DoctorRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Doctor?> GetByUserIdAsync(string userId)
    {
        return await _dbSet
            .Include(d => d.User)
            .Include(d => d.Department)
            .FirstOrDefaultAsync(d => d.UserId == userId);
    }

    public async Task<Doctor?> GetByLicenseNumberAsync(string licenseNumber)
    {
        return await _dbSet
            .Include(d => d.User)
            .Include(d => d.Department)
            .FirstOrDefaultAsync(d => d.LicenseNumber == licenseNumber);
    }

    public async Task<IEnumerable<Doctor>> GetByDepartmentAsync(int departmentId)
    {
        return await _dbSet
            .Include(d => d.User)
            .Include(d => d.Department)
            .Where(d => d.DepartmentId == departmentId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Doctor>> GetBySpecializationAsync(string specialization)
    {
        return await _dbSet
            .Include(d => d.User)
            .Include(d => d.Department)
            .Where(d => d.Specialization.Contains(specialization))
            .ToListAsync();
    }

    public async Task<IEnumerable<Doctor>> SearchAsync(string searchTerm)
    {
        return await _dbSet
            .Include(d => d.User)
            .Include(d => d.Department)
            .Where(d =>
                (d.User!.FullName != null && d.User.FullName.Contains(searchTerm)) ||
                d.Specialization.Contains(searchTerm) ||
                (d.Department != null && d.Department.Name.Contains(searchTerm)))
            .ToListAsync();
    }

    public async Task<IEnumerable<Doctor>> GetAvailableAsync()
    {
        return await _dbSet
            .Include(d => d.User)
            .Include(d => d.Department)
            .Where(d => d.IsAvailable)
            .ToListAsync();
    }

    public async Task<Doctor?> GetWithDetailsAsync(int doctorId)
    {
        return await _dbSet
            .Include(d => d.User)
            .Include(d => d.Department)
            .Include(d => d.Appointments)
            .FirstOrDefaultAsync(d => d.Id == doctorId);
    }

    public async Task<Doctor?> GetWithScheduleAsync(int doctorId)
    {
        return await _dbSet
            .Include(d => d.User)
            .Include(d => d.Department)
            .Include(d => d.Schedules)
            .FirstOrDefaultAsync(d => d.Id == doctorId);
    }
}
