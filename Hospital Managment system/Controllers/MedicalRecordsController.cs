using Hospital_Managment_system.DTOs;
using Hospital_Managment_system.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Managment_system.Controllers;

/// <summary>
/// Controller for medical record endpoints.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MedicalRecordsController : ControllerBase
{
    private readonly IMedicalRecordService _medicalRecordService;

    public MedicalRecordsController(IMedicalRecordService medicalRecordService)
    {
        _medicalRecordService = medicalRecordService;
    }

    /// <summary>
    /// Create a new medical record.
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin,Doctor")]
    public async Task<IActionResult> Create([FromBody] MedicalRecordDto medicalRecordDto)
    {
        try
        {
            var result = await _medicalRecordService.CreateMedicalRecordAsync(medicalRecordDto);
            return Created($"/api/medicalrecords/{result.Id}", new ApiResponseDto<MedicalRecordDetailDto>
            {
                Success = true,
                Message = "Medical record created successfully",
                Data = result,
                StatusCode = 201
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponseDto<string>
            {
                Success = false,
                Message = "Cannot create medical record",
                Error = ex.Message,
                StatusCode = 400
            });
        }
    }

    /// <summary>
    /// Get all medical records with pagination.
    /// </summary>
    [HttpGet]
    [Authorize(Roles = "Admin,Doctor,Receptionist")]
    public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _medicalRecordService.GetAllMedicalRecordsAsync(pageNumber, pageSize);
        return Ok(new ApiResponseDto<PaginatedListDto<MedicalRecordDetailDto>>
        {
            Success = true,
            Message = "Medical records retrieved successfully",
            Data = result,
            StatusCode = 200
        });
    }

    /// <summary>
    /// Get medical record by ID.
    /// </summary>
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,Doctor,Receptionist")]
    public async Task<IActionResult> GetById(int id)
    {
        var record = await _medicalRecordService.GetMedicalRecordAsync(id);
        if (record == null)
        {
            return NotFound(new ApiResponseDto<string>
            {
                Success = false,
                Message = "Medical record not found",
                StatusCode = 404
            });
        }

        return Ok(new ApiResponseDto<MedicalRecordDetailDto>
        {
            Success = true,
            Message = "Medical record retrieved successfully",
            Data = record,
            StatusCode = 200
        });
    }

    /// <summary>
    /// Update medical record.
    /// </summary>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Doctor")]
    public async Task<IActionResult> Update(int id, [FromBody] MedicalRecordDto medicalRecordDto)
    {
        try
        {
            var result = await _medicalRecordService.UpdateMedicalRecordAsync(id, medicalRecordDto);
            return Ok(new ApiResponseDto<MedicalRecordDetailDto>
            {
                Success = true,
                Message = "Medical record updated successfully",
                Data = result,
                StatusCode = 200
            });
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new ApiResponseDto<string>
            {
                Success = false,
                Message = "Medical record not found",
                StatusCode = 404
            });
        }
    }

    /// <summary>
    /// Delete medical record.
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _medicalRecordService.DeleteMedicalRecordAsync(id);
            return Ok(new ApiResponseDto<string>
            {
                Success = true,
                Message = "Medical record deleted successfully",
                StatusCode = 200
            });
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new ApiResponseDto<string>
            {
                Success = false,
                Message = "Medical record not found",
                StatusCode = 404
            });
        }
    }

    /// <summary>
    /// Get patient medical records.
    /// </summary>
    [HttpGet("patient/{patientId}")]
    public async Task<IActionResult> GetPatientRecords(int patientId)
    {
        var result = await _medicalRecordService.GetPatientMedicalRecordsAsync(patientId);
        return Ok(new ApiResponseDto<IList<MedicalRecordDetailDto>>
        {
            Success = true,
            Message = "Patient medical records retrieved",
            Data = result,
            StatusCode = 200
        });
    }

    /// <summary>
    /// Add prescription to medical record.
    /// </summary>
    [HttpPost("{recordId}/prescriptions")]
    [Authorize(Roles = "Admin,Doctor")]
    public async Task<IActionResult> AddPrescription(int recordId, [FromBody] PrescriptionDto prescriptionDto)
    {
        try
        {
            var result = await _medicalRecordService.AddPrescriptionAsync(recordId, prescriptionDto);
            return Created($"/api/medicalrecords/{recordId}/prescriptions/{result.Id}", new ApiResponseDto<PrescriptionDetailDto>
            {
                Success = true,
                Message = "Prescription added successfully",
                Data = result,
                StatusCode = 201
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponseDto<string>
            {
                Success = false,
                Message = "Cannot add prescription",
                Error = ex.Message,
                StatusCode = 400
            });
        }
    }

    /// <summary>
    /// Get prescriptions for medical record.
    /// </summary>
    [HttpGet("{recordId}/prescriptions")]
    [Authorize(Roles = "Admin,Doctor,Receptionist")]
    public async Task<IActionResult> GetPrescriptions(int recordId)
    {
        try
        {
            var result = await _medicalRecordService.GetPrescriptionsAsync(recordId);
            return Ok(new ApiResponseDto<IList<PrescriptionDetailDto>>
            {
                Success = true,
                Message = "Prescriptions retrieved",
                Data = result,
                StatusCode = 200
            });
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new ApiResponseDto<string>
            {
                Success = false,
                Message = "Medical record not found",
                StatusCode = 404
            });
        }
    }
}
