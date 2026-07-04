using Microsoft.EntityFrameworkCore;
using Hospital_Managment_system.Data;
using Hospital_Managment_system.Interfaces;
using Hospital_Managment_system.Models;

namespace Hospital_Managment_system.Repositories;

/// <summary>
/// Repository implementation for MedicalRecord entity.
/// </summary>
public class MedicalRecordRepository : Repository<MedicalRecord>, IMedicalRecordRepository
{
    public MedicalRecordRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<MedicalRecord>> GetPatientRecordsAsync(int patientId)
    {
        return await _dbSet
            .Include(mr => mr.Patient)
            .Include(mr => mr.Doctor)
            .Include(mr => mr.Prescriptions)
            .Where(mr => mr.PatientId == patientId)
            .OrderByDescending(mr => mr.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<MedicalRecord>> GetDoctorRecordsAsync(int doctorId)
    {
        return await _dbSet
            .Include(mr => mr.Patient)
            .Include(mr => mr.Doctor)
            .Include(mr => mr.Prescriptions)
            .Where(mr => mr.DoctorId == doctorId)
            .OrderByDescending(mr => mr.CreatedAt)
            .ToListAsync();
    }

    public async Task<MedicalRecord?> GetByAppointmentAsync(int appointmentId)
    {
        return await _dbSet
            .Include(mr => mr.Patient)
            .Include(mr => mr.Doctor)
            .Include(mr => mr.Prescriptions)
            .FirstOrDefaultAsync(mr => mr.AppointmentId == appointmentId);
    }

    public async Task<MedicalRecord?> GetWithPrescriptionsAsync(int recordId)
    {
        return await _dbSet
            .Include(mr => mr.Patient)
            .Include(mr => mr.Doctor)
            .Include(mr => mr.Prescriptions)
            .FirstOrDefaultAsync(mr => mr.Id == recordId);
    }
}
