using Microsoft.EntityFrameworkCore;
using Hospital_Managment_system.Data;
using Hospital_Managment_system.Interfaces;
using Hospital_Managment_system.Models;

namespace Hospital_Managment_system.Repositories;

/// <summary>
/// Repository implementation for Appointment entity.
/// </summary>
public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
{
    public AppointmentRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Appointment>> GetPatientAppointmentsAsync(int patientId)
    {
        return await _dbSet
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .Where(a => a.PatientId == patientId)
            .OrderByDescending(a => a.AppointmentDateTime)
            .ToListAsync();
    }

    public async Task<IEnumerable<Appointment>> GetDoctorAppointmentsAsync(int doctorId)
    {
        return await _dbSet
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .Where(a => a.DoctorId == doctorId)
            .OrderByDescending(a => a.AppointmentDateTime)
            .ToListAsync();
    }

    public async Task<IEnumerable<Appointment>> GetAppointmentsByDateAsync(int doctorId, DateTime date)
    {
        var startDate = date.Date;
        var endDate = startDate.AddDays(1);

        return await _dbSet
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .Where(a =>
                a.DoctorId == doctorId &&
                a.AppointmentDateTime >= startDate &&
                a.AppointmentDateTime < endDate)
            .OrderBy(a => a.AppointmentDateTime)
            .ToListAsync();
    }

    public async Task<IEnumerable<Appointment>> GetUpcomingAppointmentsAsync()
    {
        return await _dbSet
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .Where(a =>
                a.AppointmentDateTime > DateTime.UtcNow &&
                (a.Status == "Scheduled" || a.Status == "Rescheduled"))
            .OrderBy(a => a.AppointmentDateTime)
            .ToListAsync();
    }

    public async Task<IEnumerable<Appointment>> GetTodayAppointmentsAsync()
    {
        var today = DateTime.UtcNow.Date;
        var tomorrow = today.AddDays(1);

        return await _dbSet
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .Where(a =>
                a.AppointmentDateTime >= today &&
                a.AppointmentDateTime < tomorrow &&
                (a.Status == "Scheduled" || a.Status == "Rescheduled"))
            .OrderBy(a => a.AppointmentDateTime)
            .ToListAsync();
    }

    public async Task<bool> HasConflictAsync(int doctorId, DateTime appointmentDateTime, int durationInMinutes)
    {
        var endTime = appointmentDateTime.AddMinutes(durationInMinutes);

        return await _dbSet.AnyAsync(a =>
            a.DoctorId == doctorId &&
            (a.Status == "Scheduled" || a.Status == "Rescheduled") &&
            a.AppointmentDateTime < endTime &&
            a.AppointmentDateTime.AddMinutes(a.DurationInMinutes) > appointmentDateTime);
    }

    public async Task<Appointment?> GetWithDetailsAsync(int appointmentId)
    {
        return await _dbSet
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .Include(a => a.MedicalRecord)
            .FirstOrDefaultAsync(a => a.Id == appointmentId);
    }

    public async Task<IEnumerable<Appointment>> SearchAsync(string searchTerm)
    {
        return await _dbSet
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .Where(a =>
                (a.Patient!.User != null && a.Patient.User.FullName != null && a.Patient.User.FullName.Contains(searchTerm)) ||
                (a.Doctor!.User != null && a.Doctor.User.FullName != null && a.Doctor.User.FullName.Contains(searchTerm)) ||
                a.Status.Contains(searchTerm))
            .ToListAsync();
    }
}
