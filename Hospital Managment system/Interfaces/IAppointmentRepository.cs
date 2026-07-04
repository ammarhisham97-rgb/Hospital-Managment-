using Hospital_Managment_system.Models;

namespace Hospital_Managment_system.Interfaces;

/// <summary>
/// Specific repository for Appointment entity.
/// </summary>
public interface IAppointmentRepository : IRepository<Appointment>
{
    /// <summary>
    /// Get appointments for a patient.
    /// </summary>
    Task<IEnumerable<Appointment>> GetPatientAppointmentsAsync(int patientId);

    /// <summary>
    /// Get appointments for a doctor.
    /// </summary>
    Task<IEnumerable<Appointment>> GetDoctorAppointmentsAsync(int doctorId);

    /// <summary>
    /// Get appointments for a specific date and doctor.
    /// </summary>
    Task<IEnumerable<Appointment>> GetAppointmentsByDateAsync(int doctorId, DateTime date);

    /// <summary>
    /// Get upcoming appointments.
    /// </summary>
    Task<IEnumerable<Appointment>> GetUpcomingAppointmentsAsync();

    /// <summary>
    /// Get today's appointments.
    /// </summary>
    Task<IEnumerable<Appointment>> GetTodayAppointmentsAsync();

    /// <summary>
    /// Check if doctor has conflicting appointment.
    /// </summary>
    Task<bool> HasConflictAsync(int doctorId, DateTime appointmentDateTime, int durationInMinutes);

    /// <summary>
    /// Get appointment with all related data.
    /// </summary>
    Task<Appointment?> GetWithDetailsAsync(int appointmentId);

    /// <summary>
    /// Search appointments.
    /// </summary>
    Task<IEnumerable<Appointment>> SearchAsync(string searchTerm);
}
