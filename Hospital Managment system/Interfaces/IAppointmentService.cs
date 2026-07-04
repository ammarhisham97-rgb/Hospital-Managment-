using Hospital_Managment_system.DTOs;

namespace Hospital_Managment_system.Interfaces;

/// <summary>
/// Service interface for appointment operations.
/// </summary>
public interface IAppointmentService
{
    /// <summary>
    /// Book a new appointment.
    /// </summary>
    Task<AppointmentDetailDto> BookAppointmentAsync(AppointmentDto appointmentDto);

    /// <summary>
    /// Get appointment by ID.
    /// </summary>
    Task<AppointmentDetailDto?> GetAppointmentAsync(int appointmentId);

    /// <summary>
    /// Get all appointments with pagination.
    /// </summary>
    Task<PaginatedListDto<AppointmentDetailDto>> GetAllAppointmentsAsync(int pageNumber, int pageSize);

    /// <summary>
    /// Get patient appointments.
    /// </summary>
    Task<IList<AppointmentDetailDto>> GetPatientAppointmentsAsync(int patientId);

    /// <summary>
    /// Get doctor appointments.
    /// </summary>
    Task<IList<AppointmentDetailDto>> GetDoctorAppointmentsAsync(int doctorId);

    /// <summary>
    /// Reschedule an appointment.
    /// </summary>
    Task<AppointmentDetailDto> RescheduleAppointmentAsync(int appointmentId, RescheduleAppointmentDto reschedulDto);

    /// <summary>
    /// Cancel an appointment.
    /// </summary>
    Task<bool> CancelAppointmentAsync(int appointmentId, CancelAppointmentDto cancelDto);

    /// <summary>
    /// Get today's appointments.
    /// </summary>
    Task<IList<AppointmentDetailDto>> GetTodayAppointmentsAsync();

    /// <summary>
    /// Get upcoming appointments.
    /// </summary>
    Task<IList<AppointmentDetailDto>> GetUpcomingAppointmentsAsync();

    /// <summary>
    /// Get appointments for a specific date and doctor.
    /// </summary>
    Task<IList<AppointmentDetailDto>> GetAppointmentsByDateAsync(int doctorId, DateTime date);

    /// <summary>
    /// Mark appointment as completed.
    /// </summary>
    Task<bool> CompleteAppointmentAsync(int appointmentId);

    /// <summary>
    /// Search appointments.
    /// </summary>
    Task<IList<AppointmentDetailDto>> SearchAppointmentsAsync(string searchTerm);
}
