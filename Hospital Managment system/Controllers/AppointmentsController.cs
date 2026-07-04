using Hospital_Managment_system.DTOs;
using Hospital_Managment_system.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Managment_system.Controllers;

/// <summary>
/// Controller for appointment endpoints.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AppointmentsController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;

    public AppointmentsController(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    /// <summary>
    /// Book a new appointment.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Book([FromBody] AppointmentDto appointmentDto)
    {
        try
        {
            var result = await _appointmentService.BookAppointmentAsync(appointmentDto);
            return Created($"/api/appointments/{result.Id}", new ApiResponseDto<AppointmentDetailDto>
            {
                Success = true,
                Message = "Appointment booked successfully",
                Data = result,
                StatusCode = 201
            });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new ApiResponseDto<string>
            {
                Success = false,
                Message = "Cannot book appointment",
                Error = ex.Message,
                StatusCode = 400
            });
        }
    }

    /// <summary>
    /// Get all appointments with pagination.
    /// </summary>
    [HttpGet]
    [Authorize(Roles = "Admin,Doctor,Receptionist")]
    public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _appointmentService.GetAllAppointmentsAsync(pageNumber, pageSize);
        return Ok(new ApiResponseDto<PaginatedListDto<AppointmentDetailDto>>
        {
            Success = true,
            Message = "Appointments retrieved successfully",
            Data = result,
            StatusCode = 200
        });
    }

    /// <summary>
    /// Get appointment by ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var appointment = await _appointmentService.GetAppointmentAsync(id);
        if (appointment == null)
        {
            return NotFound(new ApiResponseDto<string>
            {
                Success = false,
                Message = "Appointment not found",
                StatusCode = 404
            });
        }

        return Ok(new ApiResponseDto<AppointmentDetailDto>
        {
            Success = true,
            Message = "Appointment retrieved successfully",
            Data = appointment,
            StatusCode = 200
        });
    }

    /// <summary>
    /// Reschedule an appointment.
    /// </summary>
    [HttpPut("{id}/reschedule")]
    public async Task<IActionResult> Reschedule(int id, [FromBody] RescheduleAppointmentDto rescheduleDto)
    {
        try
        {
            var result = await _appointmentService.RescheduleAppointmentAsync(id, rescheduleDto);
            return Ok(new ApiResponseDto<AppointmentDetailDto>
            {
                Success = true,
                Message = "Appointment rescheduled successfully",
                Data = result,
                StatusCode = 200
            });
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new ApiResponseDto<string>
            {
                Success = false,
                Message = "Appointment not found",
                StatusCode = 404
            });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new ApiResponseDto<string>
            {
                Success = false,
                Message = "Cannot reschedule appointment",
                Error = ex.Message,
                StatusCode = 400
            });
        }
    }

    /// <summary>
    /// Cancel an appointment.
    /// </summary>
    [HttpPut("{id}/cancel")]
    public async Task<IActionResult> Cancel(int id, [FromBody] CancelAppointmentDto cancelDto)
    {
        try
        {
            await _appointmentService.CancelAppointmentAsync(id, cancelDto);
            return Ok(new ApiResponseDto<string>
            {
                Success = true,
                Message = "Appointment cancelled successfully",
                StatusCode = 200
            });
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new ApiResponseDto<string>
            {
                Success = false,
                Message = "Appointment not found",
                StatusCode = 404
            });
        }
    }

    /// <summary>
    /// Get today's appointments.
    /// </summary>
    [HttpGet("today/list")]
    [Authorize(Roles = "Admin,Doctor,Receptionist")]
    public async Task<IActionResult> GetTodayAppointments()
    {
        var result = await _appointmentService.GetTodayAppointmentsAsync();
        return Ok(new ApiResponseDto<IList<AppointmentDetailDto>>
        {
            Success = true,
            Message = "Today's appointments retrieved",
            Data = result,
            StatusCode = 200
        });
    }

    /// <summary>
    /// Get upcoming appointments.
    /// </summary>
    [HttpGet("upcoming/list")]
    [Authorize(Roles = "Admin,Doctor,Receptionist")]
    public async Task<IActionResult> GetUpcomingAppointments()
    {
        var result = await _appointmentService.GetUpcomingAppointmentsAsync();
        return Ok(new ApiResponseDto<IList<AppointmentDetailDto>>
        {
            Success = true,
            Message = "Upcoming appointments retrieved",
            Data = result,
            StatusCode = 200
        });
    }

    /// <summary>
    /// Get patient appointments.
    /// </summary>
    [HttpGet("patient/{patientId}")]
    public async Task<IActionResult> GetPatientAppointments(int patientId)
    {
        var result = await _appointmentService.GetPatientAppointmentsAsync(patientId);
        return Ok(new ApiResponseDto<IList<AppointmentDetailDto>>
        {
            Success = true,
            Message = "Patient appointments retrieved",
            Data = result,
            StatusCode = 200
        });
    }

    /// <summary>
    /// Get doctor appointments.
    /// </summary>
    [HttpGet("doctor/{doctorId}")]
    [Authorize(Roles = "Admin,Doctor,Receptionist")]
    public async Task<IActionResult> GetDoctorAppointments(int doctorId)
    {
        var result = await _appointmentService.GetDoctorAppointmentsAsync(doctorId);
        return Ok(new ApiResponseDto<IList<AppointmentDetailDto>>
        {
            Success = true,
            Message = "Doctor appointments retrieved",
            Data = result,
            StatusCode = 200
        });
    }

    /// <summary>
    /// Complete an appointment.
    /// </summary>
    [HttpPut("{id}/complete")]
    [Authorize(Roles = "Admin,Doctor")]
    public async Task<IActionResult> CompleteAppointment(int id)
    {
        try
        {
            await _appointmentService.CompleteAppointmentAsync(id);
            return Ok(new ApiResponseDto<string>
            {
                Success = true,
                Message = "Appointment marked as completed",
                StatusCode = 200
            });
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new ApiResponseDto<string>
            {
                Success = false,
                Message = "Appointment not found",
                StatusCode = 404
            });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new ApiResponseDto<string>
            {
                Success = false,
                Message = "Cannot complete appointment",
                Error = ex.Message,
                StatusCode = 400
            });
        }
    }
}
