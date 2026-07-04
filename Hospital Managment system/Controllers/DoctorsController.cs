using Hospital_Managment_system.DTOs;
using Hospital_Managment_system.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Managment_system.Controllers;

/// <summary>
/// Controller for doctor endpoints.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DoctorsController : ControllerBase
{
    private readonly IDoctorService _doctorService;

    public DoctorsController(IDoctorService doctorService)
    {
        _doctorService = doctorService;
    }

    /// <summary>
    /// Get all doctors with pagination.
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _doctorService.GetAllDoctorsAsync(pageNumber, pageSize);
        return Ok(new ApiResponseDto<PaginatedListDto<DoctorDetailDto>>
        {
            Success = true,
            Message = "Doctors retrieved successfully",
            Data = result,
            StatusCode = 200
        });
    }

    /// <summary>
    /// Get doctor by ID.
    /// </summary>
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(int id)
    {
        var doctor = await _doctorService.GetDoctorAsync(id);
        if (doctor == null)
        {
            return NotFound(new ApiResponseDto<string>
            {
                Success = false,
                Message = "Doctor not found",
                StatusCode = 404
            });
        }

        return Ok(new ApiResponseDto<DoctorDetailDto>
        {
            Success = true,
            Message = "Doctor retrieved successfully",
            Data = doctor,
            StatusCode = 200
        });
    }

    /// <summary>
    /// Update doctor information.
    /// </summary>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin, Doctor")]
    public async Task<IActionResult> Update(int id, [FromBody] DoctorDto doctorDto)
    {
        try
        {
            var result = await _doctorService.UpdateDoctorAsync(id, doctorDto);
            return Ok(new ApiResponseDto<DoctorDetailDto>
            {
                Success = true,
                Message = "Doctor updated successfully",
                Data = result,
                StatusCode = 200
            });
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new ApiResponseDto<string>
            {
                Success = false,
                Message = "Doctor not found",
                StatusCode = 404
            });
        }
    }

    /// <summary>
    /// Delete doctor.
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _doctorService.DeleteDoctorAsync(id);
            return Ok(new ApiResponseDto<string>
            {
                Success = true,
                Message = "Doctor deleted successfully",
                StatusCode = 200
            });
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new ApiResponseDto<string>
            {
                Success = false,
                Message = "Doctor not found",
                StatusCode = 404
            });
        }
    }

    /// <summary>
    /// Get doctors by specialization.
    /// </summary>
    [HttpGet("specialization/{specialization}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetBySpecialization(string specialization)
    {
        var result = await _doctorService.GetDoctorsBySpecializationAsync(specialization);
        return Ok(new ApiResponseDto<IList<DoctorDetailDto>>
        {
            Success = true,
            Message = "Doctors found",
            Data = result,
            StatusCode = 200
        });
    }

    /// <summary>
    /// Get available doctors.
    /// </summary>
    [HttpGet("available/list")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAvailable()
    {
        var result = await _doctorService.GetAvailableDoctorsAsync();
        return Ok(new ApiResponseDto<IList<DoctorDetailDto>>
        {
            Success = true,
            Message = "Available doctors retrieved",
            Data = result,
            StatusCode = 200
        });
    }

    /// <summary>
    /// Search doctors.
    /// </summary>
    [HttpGet("search/{searchTerm}")]
    [AllowAnonymous]
    public async Task<IActionResult> Search(string searchTerm)
    {
        var result = await _doctorService.SearchDoctorsAsync(searchTerm);
        return Ok(new ApiResponseDto<IList<DoctorDetailDto>>
        {
            Success = true,
            Message = "Doctors found",
            Data = result,
            StatusCode = 200
        });
    }

    /// <summary>
    /// Get doctor schedule.
    /// </summary>
    [HttpGet("{id}/schedule")]
    [AllowAnonymous]
    public async Task<IActionResult> GetSchedule(int id)
    {
        try
        {
            var result = await _doctorService.GetDoctorScheduleAsync(id);
            return Ok(new ApiResponseDto<IList<DoctorScheduleDto>>
            {
                Success = true,
                Message = "Schedule retrieved",
                Data = result,
                StatusCode = 200
            });
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new ApiResponseDto<string>
            {
                Success = false,
                Message = "Doctor not found",
                StatusCode = 404
            });
        }
    }

    /// <summary>
    /// Add doctor schedule.
    /// </summary>
    [HttpPost("{id}/schedule")]
    [Authorize(Roles = "Admin, Doctor")]
    public async Task<IActionResult> AddSchedule(int id, [FromBody] DoctorScheduleDto scheduleDto)
    {
        try
        {
            var result = await _doctorService.AddDoctorScheduleAsync(id, scheduleDto);
            return Ok(new ApiResponseDto<DoctorScheduleDto>
            {
                Success = true,
                Message = "Schedule added successfully",
                Data = result,
                StatusCode = 200
            });
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new ApiResponseDto<string>
            {
                Success = false,
                Message = "Doctor not found",
                StatusCode = 404
            });
        }
    }
}
