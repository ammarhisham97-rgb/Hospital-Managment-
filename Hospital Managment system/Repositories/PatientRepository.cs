using Microsoft.EntityFrameworkCore;
using Hospital_Managment_system.Data;
using Hospital_Managment_system.Interfaces;
using Hospital_Managment_system.Models;

namespace Hospital_Managment_system.Repositories;

/// <summary>
/// Repository implementation for Patient entity.
/// </summary>
public class PatientRepository : Repository<Patient>, IPatientRepository
{
    public PatientRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Patient?> GetByUserIdAsync(string userId)
    {
        return await _dbSet
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.UserId == userId);
    }

    public async Task<Patient?> GetByPatientNumberAsync(string patientNumber)
    {
        return await _dbSet
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.PatientNumber == patientNumber);
    }

    public async Task<IEnumerable<Patient>> SearchAsync(string searchTerm)
    {
        return await _dbSet
            .Include(p => p.User)
            .Where(p => 
                (p.User!.FullName != null && p.User.FullName.Contains(searchTerm)) ||
                (p.User!.Email != null && p.User.Email.Contains(searchTerm)) ||
                (p.User!.PhoneNumber != null && p.User.PhoneNumber.Contains(searchTerm)) ||
                p.PatientNumber.Contains(searchTerm))
            .ToListAsync();
    }

    public async Task<Patient?> GetWithDetailsAsync(int patientId)
    {
        return await _dbSet
            .Include(p => p.User)
            .Include(p => p.Appointments)
            .Include(p => p.MedicalRecords)
            .FirstOrDefaultAsync(p => p.Id == patientId);
    }
}
