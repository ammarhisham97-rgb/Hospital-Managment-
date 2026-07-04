using Hospital_Managment_system.DTOs;
using Hospital_Managment_system.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Managment_system.Controllers;

/// <summary>
/// Controller for patient endpoints.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PatientsController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientsController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    /// <summary>
    /// Get all patients with pagination.
    /// </summary>
    [HttpGet]
    [Authorize(Roles = "Admin,Doctor,Receptionist")]
    public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _patientService.GetAllPatientsAsync(pageNumber, pageSize);
        return Ok(new ApiResponseDto<PaginatedListDto<PatientDetailDto>>
        {
            Success = true,
            Message = "Patients retrieved successfully",
            Data = result,
            StatusCode = 200
        });
    }

    /// <summary>
    /// Get patient by ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var patient = await _patientService.GetPatientAsync(id);
        if (patient == null)
        {
            return NotFound(new ApiResponseDto<string>
            {
                Success = false,
                Message = "Patient not found",
                StatusCode = 404
            });
        }

        return Ok(new ApiResponseDto<PatientDetailDto>
        {
            Success = true,
            Message = "Patient retrieved successfully",
            Data = patient,
            StatusCode = 200
        });
    }

    /// <summary>
    /// Update patient information.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] PatientDto patientDto)
    {
        try
        {
            var result = await _patientService.UpdatePatientAsync(id, patientDto);
            return Ok(new ApiResponseDto<PatientDetailDto>
            {
                Success = true,
                Message = "Patient updated successfully",
                Data = result,
                StatusCode = 200
            });
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new ApiResponseDto<string>
            {
                Success = false,
                Message = "Patient not found",
                StatusCode = 404
            });
        }
    }

    /// <summary>
    /// Delete patient.
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,Receptionist")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _patientService.DeletePatientAsync(id);
            return Ok(new ApiResponseDto<string>
            {
                Success = true,
                Message = "Patient deleted successfully",
                StatusCode = 200
            });
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new ApiResponseDto<string>
            {
                Success = false,
                Message = "Patient not found",
                StatusCode = 404
            });
        }
    }

    /// <summary>
    /// Search patients.
    /// </summary>
    [HttpGet("search/{searchTerm}")]
    [Authorize(Roles = "Admin,Doctor,Receptionist")]
    public async Task<IActionResult> Search(string searchTerm)
    {
        var result = await _patientService.SearchPatientsAsync(searchTerm);
        return Ok(new ApiResponseDto<IList<PatientSearchDto>>
        {
            Success = true,
            Message = "Patients found",
            Data = result,
            StatusCode = 200
        });
    }

    /// <summary>
    /// Get patient medical history.
    /// </summary>
    [HttpGet("{id}/medical-history")]
    public async Task<IActionResult> GetMedicalHistory(int id)
    {
        var result = await _patientService.GetPatientMedicalHistoryAsync(id);
        return Ok(new ApiResponseDto<IList<MedicalRecordDetailDto>>
        {
            Success = true,
            Message = "Medical history retrieved",
            Data = result,
            StatusCode = 200
        });
    }
}
