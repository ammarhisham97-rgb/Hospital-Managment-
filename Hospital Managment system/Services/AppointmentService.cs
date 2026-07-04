using AutoMapper;
using Hospital_Managment_system.DTOs;
using Hospital_Managment_system.Helpers;
using Hospital_Managment_system.Interfaces;
using Hospital_Managment_system.Models;
using Serilog;

namespace Hospital_Managment_system.Services;

/// <summary>
/// Service for appointment operations.
/// </summary>
public class AppointmentService : IAppointmentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AppointmentService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<AppointmentDetailDto> BookAppointmentAsync(AppointmentDto appointmentDto)
    {
        // Check for conflicts
        if (await _unitOfWork.Appointments.HasConflictAsync(
            appointmentDto.DoctorId,
            appointmentDto.AppointmentDateTime,
            appointmentDto.DurationInMinutes))
        {
            throw new InvalidOperationException("Doctor has a conflicting appointment at this time");
        }

        var appointment = _mapper.Map<Appointment>(appointmentDto);
        appointment.Status = "Scheduled";
        appointment.CreatedAt = DateTime.UtcNow;
        appointment.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.Appointments.AddAsync(appointment);
        await _unitOfWork.SaveChangesAsync();

        Log.Information("Appointment booked with ID: {AppointmentId}", appointment.Id);
        return _mapper.Map<AppointmentDetailDto>(await _unitOfWork.Appointments.GetWithDetailsAsync(appointment.Id));
    }

    public async Task<AppointmentDetailDto?> GetAppointmentAsync(int appointmentId)
    {
        var appointment = await _unitOfWork.Appointments.GetWithDetailsAsync(appointmentId);
        return appointment == null ? null : _mapper.Map<AppointmentDetailDto>(appointment);
    }

    public async Task<PaginatedListDto<AppointmentDetailDto>> GetAllAppointmentsAsync(int pageNumber, int pageSize)
    {
        var (validPageNumber, validPageSize) = PaginationHelper.ValidatePageParameters(pageNumber, pageSize);
        var (appointments, totalCount) = await _unitOfWork.Appointments.GetPaginatedAsync(validPageNumber, validPageSize);
        var appointmentDtos = _mapper.Map<IList<AppointmentDetailDto>>(appointments);

        return new PaginatedListDto<AppointmentDetailDto>
        {
            Items = appointmentDtos,
            Pagination = new PaginationDto
            {
                PageNumber = validPageNumber,
                PageSize = validPageSize,
                TotalItems = totalCount,
                TotalPages = PaginationHelper.CalculateTotalPages(totalCount, validPageSize)
            }
        };
    }

    public async Task<IList<AppointmentDetailDto>> GetPatientAppointmentsAsync(int patientId)
    {
        var appointments = await _unitOfWork.Appointments.GetPatientAppointmentsAsync(patientId);
        return _mapper.Map<IList<AppointmentDetailDto>>(appointments);
    }

    public async Task<IList<AppointmentDetailDto>> GetDoctorAppointmentsAsync(int doctorId)
    {
        var appointments = await _unitOfWork.Appointments.GetDoctorAppointmentsAsync(doctorId);
        return _mapper.Map<IList<AppointmentDetailDto>>(appointments);
    }

    public async Task<AppointmentDetailDto> RescheduleAppointmentAsync(int appointmentId, RescheduleAppointmentDto reschedulDto)
    {
        var appointment = await _unitOfWork.Appointments.GetByIdAsync(appointmentId)
            ?? throw new KeyNotFoundException($"Appointment with ID {appointmentId} not found");

        if (appointment.Status == "Cancelled")
            throw new InvalidOperationException("Cannot reschedule a cancelled appointment");

        if (await _unitOfWork.Appointments.HasConflictAsync(
            appointment.DoctorId,
            reschedulDto.NewAppointmentDateTime,
            appointment.DurationInMinutes))
        {
            throw new InvalidOperationException("Doctor has a conflicting appointment at this time");
        }

        appointment.AppointmentDateTime = reschedulDto.NewAppointmentDateTime;
        appointment.Status = "Rescheduled";
        appointment.Notes = $"Rescheduled: {reschedulDto.Reason}";
        appointment.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.Appointments.UpdateAsync(appointment);
        await _unitOfWork.SaveChangesAsync();

        Log.Information("Appointment rescheduled with ID: {AppointmentId}", appointmentId);
        return _mapper.Map<AppointmentDetailDto>(await _unitOfWork.Appointments.GetWithDetailsAsync(appointmentId));
    }

    public async Task<bool> CancelAppointmentAsync(int appointmentId, CancelAppointmentDto cancelDto)
    {
        var appointment = await _unitOfWork.Appointments.GetByIdAsync(appointmentId)
            ?? throw new KeyNotFoundException($"Appointment with ID {appointmentId} not found");

        appointment.Status = "Cancelled";
        appointment.Notes = $"Cancelled: {cancelDto.Reason}";
        appointment.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.Appointments.UpdateAsync(appointment);
        await _unitOfWork.SaveChangesAsync();

        Log.Information("Appointment cancelled with ID: {AppointmentId}", appointmentId);
        return true;
    }

    public async Task<IList<AppointmentDetailDto>> GetTodayAppointmentsAsync()
    {
        var appointments = await _unitOfWork.Appointments.GetTodayAppointmentsAsync();
        return _mapper.Map<IList<AppointmentDetailDto>>(appointments);
    }

    public async Task<IList<AppointmentDetailDto>> GetUpcomingAppointmentsAsync()
    {
        var appointments = await _unitOfWork.Appointments.GetUpcomingAppointmentsAsync();
        return _mapper.Map<IList<AppointmentDetailDto>>(appointments);
    }

    public async Task<IList<AppointmentDetailDto>> GetAppointmentsByDateAsync(int doctorId, DateTime date)
    {
        var appointments = await _unitOfWork.Appointments.GetAppointmentsByDateAsync(doctorId, date);
        return _mapper.Map<IList<AppointmentDetailDto>>(appointments);
    }

    public async Task<bool> CompleteAppointmentAsync(int appointmentId)
    {
        var appointment = await _unitOfWork.Appointments.GetByIdAsync(appointmentId)
            ?? throw new KeyNotFoundException($"Appointment with ID {appointmentId} not found");

        if (appointment.Status == "Cancelled")
            throw new InvalidOperationException("Cannot complete a cancelled appointment");

        appointment.Status = "Completed";
        appointment.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.Appointments.UpdateAsync(appointment);
        await _unitOfWork.SaveChangesAsync();

        Log.Information("Appointment completed with ID: {AppointmentId}", appointmentId);
        return true;
    }

    public async Task<IList<AppointmentDetailDto>> SearchAppointmentsAsync(string searchTerm)
    {
        var appointments = await _unitOfWork.Appointments.SearchAsync(searchTerm);
        return _mapper.Map<IList<AppointmentDetailDto>>(appointments);
    }
}
