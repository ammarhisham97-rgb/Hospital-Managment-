using Microsoft.EntityFrameworkCore;
using Hospital_Managment_system.Data;
using Hospital_Managment_system.Interfaces;
using Hospital_Managment_system.Models;

namespace Hospital_Managment_system.Repositories;

/// <summary>
/// Repository implementation for Department entity.
/// </summary>
public class DepartmentRepository : Repository<Department>, IDepartmentRepository
{
    public DepartmentRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Department?> GetByNameAsync(string name)
    {
        return await _dbSet
            .Include(d => d.Doctors)
            .FirstOrDefaultAsync(d => d.Name == name);
    }

    public async Task<Department?> GetWithDoctorsAsync(int departmentId)
    {
        return await _dbSet
            .Include(d => d.Doctors)
            .FirstOrDefaultAsync(d => d.Id == departmentId);
    }

    public async Task<IEnumerable<Department>> SearchAsync(string searchTerm)
    {
        return await _dbSet
            .Include(d => d.Doctors)
            .Where(d =>
                d.Name.Contains(searchTerm) ||
                (d.Description != null && d.Description.Contains(searchTerm)) ||
                (d.Email != null && d.Email.Contains(searchTerm)))
            .ToListAsync();
    }
}
