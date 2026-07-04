using Hospital_Managment_system.DTOs;
using Hospital_Managment_system.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Managment_system.Controllers;

/// <summary>
/// Controller for department endpoints.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DepartmentsController : ControllerBase
{
    private readonly IDepartmentService _departmentService;

    public DepartmentsController(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    /// <summary>
    /// Create a new department.
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] DepartmentDto departmentDto)
    {
        try
        {
            var result = await _departmentService.CreateDepartmentAsync(departmentDto);
            return Created($"/api/departments/{result.Id}", new ApiResponseDto<DepartmentDetailDto>
            {
                Success = true,
                Message = "Department created successfully",
                Data = result,
                StatusCode = 201
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponseDto<string>
            {
                Success = false,
                Message = "Cannot create department",
                Error = ex.Message,
                StatusCode = 400
            });
        }
    }

    /// <summary>
    /// Get all departments with pagination.
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _departmentService.GetAllDepartmentsAsync(pageNumber, pageSize);
        return Ok(new ApiResponseDto<PaginatedListDto<DepartmentDetailDto>>
        {
            Success = true,
            Message = "Departments retrieved successfully",
            Data = result,
            StatusCode = 200
        });
    }

    /// <summary>
    /// Get department by ID.
    /// </summary>
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(int id)
    {
        var department = await _departmentService.GetDepartmentAsync(id);
        if (department == null)
        {
            return NotFound(new ApiResponseDto<string>
            {
                Success = false,
                Message = "Department not found",
                StatusCode = 404
            });
        }

        return Ok(new ApiResponseDto<DepartmentDetailDto>
        {
            Success = true,
            Message = "Department retrieved successfully",
            Data = department,
            StatusCode = 200
        });
    }

    /// <summary>
    /// Update department.
    /// </summary>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] DepartmentDto departmentDto)
    {
        try
        {
            var result = await _departmentService.UpdateDepartmentAsync(id, departmentDto);
            return Ok(new ApiResponseDto<DepartmentDetailDto>
            {
                Success = true,
                Message = "Department updated successfully",
                Data = result,
                StatusCode = 200
            });
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new ApiResponseDto<string>
            {
                Success = false,
                Message = "Department not found",
                StatusCode = 404
            });
        }
    }

    /// <summary>
    /// Delete department.
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _departmentService.DeleteDepartmentAsync(id);
            return Ok(new ApiResponseDto<string>
            {
                Success = true,
                Message = "Department deleted successfully",
                StatusCode = 200
            });
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new ApiResponseDto<string>
            {
                Success = false,
                Message = "Department not found",
                StatusCode = 404
            });
        }
    }

    /// <summary>
    /// Search departments.
    /// </summary>
    [HttpGet("search/{searchTerm}")]
    [AllowAnonymous]
    public async Task<IActionResult> Search(string searchTerm)
    {
        var result = await _departmentService.SearchDepartmentsAsync(searchTerm);
        return Ok(new ApiResponseDto<IList<DepartmentDetailDto>>
        {
            Success = true,
            Message = "Departments found",
            Data = result,
            StatusCode = 200
        });
    }
}
