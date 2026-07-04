using Hospital_Managment_system.DTOs;

namespace Hospital_Managment_system.Interfaces;

/// <summary>
/// Service interface for dashboard operations.
/// </summary>
public interface IDashboardService
{
    /// <summary>
    /// Get total patient count.
    /// </summary>
    Task<int> GetTotalPatientsAsync();

    /// <summary>
    /// Get total doctor count.
    /// </summary>
    Task<int> GetTotalDoctorsAsync();

    /// <summary>
    /// Get total appointment count.
    /// </summary>
    Task<int> GetTotalAppointmentsAsync();

    /// <summary>
    /// Get today's appointment count.
    /// </summary>
    Task<int> GetTodayAppointmentsCountAsync();

    /// <summary>
    /// Get upcoming appointment count.
    /// </summary>
    Task<int> GetUpcomingAppointmentsCountAsync();

    /// <summary>
    /// Get total department count.
    /// </summary>
    Task<int> GetTotalDepartmentsAsync();

    /// <summary>
    /// Get today's appointments.
    /// </summary>
    Task<IList<AppointmentDetailDto>> GetTodayAppointmentsAsync();

    /// <summary>
    /// Get upcoming appointments.
    /// </summary>
    Task<IList<AppointmentDetailDto>> GetUpcomingAppointmentsAsync();
}
