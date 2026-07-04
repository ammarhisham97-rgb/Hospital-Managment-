using Hospital_Managment_system.DTOs;
using Hospital_Managment_system.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Managment_system.Controllers;

/// <summary>
/// Controller for dashboard statistics endpoints.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin,Receptionist")]
public class DashboardController : ControllerBase
{
    private readonly IDashboardService _dashboardService;

    public DashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    /// <summary>
    /// Get dashboard statistics.
    /// </summary>
    [HttpGet("statistics")]
    public async Task<IActionResult> GetStatistics()
    {
        var totalPatients = await _dashboardService.GetTotalPatientsAsync();
        var totalDoctors = await _dashboardService.GetTotalDoctorsAsync();
        var totalAppointments = await _dashboardService.GetTotalAppointmentsAsync();
        var totalDepartments = await _dashboardService.GetTotalDepartmentsAsync();
        var todayAppointments = await _dashboardService.GetTodayAppointmentsCountAsync();
        var upcomingAppointments = await _dashboardService.GetUpcomingAppointmentsCountAsync();

        var statistics = new
        {
            TotalPatients = totalPatients,
            TotalDoctors = totalDoctors,
            TotalAppointments = totalAppointments,
            TotalDepartments = totalDepartments,
            TodayAppointmentsCount = todayAppointments,
            UpcomingAppointmentsCount = upcomingAppointments
        };

        return Ok(new ApiResponseDto<object>
        {
            Success = true,
            Message = "Dashboard statistics retrieved successfully",
            Data = statistics,
            StatusCode = 200
        });
    }

    /// <summary>
    /// Get total patients count.
    /// </summary>
    [HttpGet("total-patients")]
    public async Task<IActionResult> GetTotalPatients()
    {
        var count = await _dashboardService.GetTotalPatientsAsync();
        return Ok(new ApiResponseDto<int>
        {
            Success = true,
            Message = "Total patients count retrieved",
            Data = count,
            StatusCode = 200
        });
    }

    /// <summary>
    /// Get total doctors count.
    /// </summary>
    [HttpGet("total-doctors")]
    public async Task<IActionResult> GetTotalDoctors()
    {
        var count = await _dashboardService.GetTotalDoctorsAsync();
        return Ok(new ApiResponseDto<int>
        {
            Success = true,
            Message = "Total doctors count retrieved",
            Data = count,
            StatusCode = 200
        });
    }

    /// <summary>
    /// Get total appointments count.
    /// </summary>
    [HttpGet("total-appointments")]
    public async Task<IActionResult> GetTotalAppointments()
    {
        var count = await _dashboardService.GetTotalAppointmentsAsync();
        return Ok(new ApiResponseDto<int>
        {
            Success = true,
            Message = "Total appointments count retrieved",
            Data = count,
            StatusCode = 200
        });
    }

    /// <summary>
    /// Get today's appointments count.
    /// </summary>
    [HttpGet("today-appointments-count")]
    public async Task<IActionResult> GetTodayAppointmentsCount()
    {
        var count = await _dashboardService.GetTodayAppointmentsCountAsync();
        return Ok(new ApiResponseDto<int>
        {
            Success = true,
            Message = "Today's appointments count retrieved",
            Data = count,
            StatusCode = 200
        });
    }

    /// <summary>
    /// Get today's appointments list.
    /// </summary>
    [HttpGet("today-appointments")]
    public async Task<IActionResult> GetTodayAppointments()
    {
        var appointments = await _dashboardService.GetTodayAppointmentsAsync();
        return Ok(new ApiResponseDto<IList<AppointmentDetailDto>>
        {
            Success = true,
            Message = "Today's appointments retrieved",
            Data = appointments,
            StatusCode = 200
        });
    }

    /// <summary>
    /// Get upcoming appointments count.
    /// </summary>
    [HttpGet("upcoming-appointments-count")]
    public async Task<IActionResult> GetUpcomingAppointmentsCount()
    {
        var count = await _dashboardService.GetUpcomingAppointmentsCountAsync();
        return Ok(new ApiResponseDto<int>
        {
            Success = true,
            Message = "Upcoming appointments count retrieved",
            Data = count,
            StatusCode = 200
        });
    }

    /// <summary>
    /// Get upcoming appointments list.
    /// </summary>
    [HttpGet("upcoming-appointments")]
    public async Task<IActionResult> GetUpcomingAppointments()
    {
        var appointments = await _dashboardService.GetUpcomingAppointmentsAsync();
        return Ok(new ApiResponseDto<IList<AppointmentDetailDto>>
        {
            Success = true,
            Message = "Upcoming appointments retrieved",
            Data = appointments,
            StatusCode = 200
        });
    }

    /// <summary>
    /// Get total departments count.
    /// </summary>
    [HttpGet("total-departments")]
    public async Task<IActionResult> GetTotalDepartments()
    {
        var count = await _dashboardService.GetTotalDepartmentsAsync();
        return Ok(new ApiResponseDto<int>
        {
            Success = true,
            Message = "Total departments count retrieved",
            Data = count,
            StatusCode = 200
        });
    }
}
