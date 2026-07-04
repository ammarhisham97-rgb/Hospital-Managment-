using AutoMapper;
using Hospital_Managment_system.DTOs;
using Hospital_Managment_system.Helpers;
using Hospital_Managment_system.Interfaces;
using Serilog;

namespace Hospital_Managment_system.Services;

/// <summary>
/// Service for department operations.
/// </summary>
public class DepartmentService : IDepartmentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DepartmentService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<DepartmentDetailDto> CreateDepartmentAsync(DepartmentDto departmentDto)
    {
        var department = _mapper.Map<Models.Department>(departmentDto);
        department.CreatedAt = DateTime.UtcNow;
        department.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.Departments.AddAsync(department);
        await _unitOfWork.SaveChangesAsync();

        Log.Information("Department created with ID: {DepartmentId}", department.Id);
        return _mapper.Map<DepartmentDetailDto>(await _unitOfWork.Departments.GetWithDoctorsAsync(department.Id));
    }

    public async Task<DepartmentDetailDto?> GetDepartmentAsync(int departmentId)
    {
        var department = await _unitOfWork.Departments.GetWithDoctorsAsync(departmentId);
        return department == null ? null : _mapper.Map<DepartmentDetailDto>(department);
    }

    public async Task<PaginatedListDto<DepartmentDetailDto>> GetAllDepartmentsAsync(int pageNumber, int pageSize)
    {
        var (validPageNumber, validPageSize) = PaginationHelper.ValidatePageParameters(pageNumber, pageSize);
        var (departments, totalCount) = await _unitOfWork.Departments.GetPaginatedAsync(validPageNumber, validPageSize);
        var departmentDtos = _mapper.Map<IList<DepartmentDetailDto>>(departments);

        return new PaginatedListDto<DepartmentDetailDto>
        {
            Items = departmentDtos,
            Pagination = new PaginationDto
            {
                PageNumber = validPageNumber,
                PageSize = validPageSize,
                TotalItems = totalCount,
                TotalPages = PaginationHelper.CalculateTotalPages(totalCount, validPageSize)
            }
        };
    }

    public async Task<DepartmentDetailDto> UpdateDepartmentAsync(int departmentId, DepartmentDto departmentDto)
    {
        var department = await _unitOfWork.Departments.GetByIdAsync(departmentId)
            ?? throw new KeyNotFoundException($"Department with ID {departmentId} not found");

        _mapper.Map(departmentDto, department);
        department.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.Departments.UpdateAsync(department);
        await _unitOfWork.SaveChangesAsync();

        Log.Information("Department updated with ID: {DepartmentId}", departmentId);
        return _mapper.Map<DepartmentDetailDto>(await _unitOfWork.Departments.GetWithDoctorsAsync(departmentId));
    }

    public async Task<bool> DeleteDepartmentAsync(int departmentId)
    {
        var department = await _unitOfWork.Departments.GetByIdAsync(departmentId)
            ?? throw new KeyNotFoundException($"Department with ID {departmentId} not found");

        department.IsDeleted = true;
        department.DeletedAt = DateTime.UtcNow;

        await _unitOfWork.Departments.UpdateAsync(department);
        await _unitOfWork.SaveChangesAsync();

        Log.Information("Department soft-deleted with ID: {DepartmentId}", departmentId);
        return true;
    }

    public async Task<IList<DepartmentDetailDto>> SearchDepartmentsAsync(string searchTerm)
    {
        var departments = await _unitOfWork.Departments.SearchAsync(searchTerm);
        return _mapper.Map<IList<DepartmentDetailDto>>(departments);
    }

    public async Task<DepartmentDetailDto?> GetDepartmentWithDoctorsAsync(int departmentId)
    {
        var department = await _unitOfWork.Departments.GetWithDoctorsAsync(departmentId);
        return department == null ? null : _mapper.Map<DepartmentDetailDto>(department);
    }
}

/// <summary>
/// Service for dashboard statistics.
/// </summary>
public class DashboardService : IDashboardService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DashboardService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<int> GetTotalPatientsAsync()
    {
        var patients = await _unitOfWork.Patients.GetAllAsync();
        return patients.Count();
    }

    public async Task<int> GetTotalDoctorsAsync()
    {
        var doctors = await _unitOfWork.Doctors.GetAllAsync();
        return doctors.Count();
    }

    public async Task<int> GetTotalAppointmentsAsync()
    {
        var appointments = await _unitOfWork.Appointments.GetAllAsync();
        return appointments.Count();
    }

    public async Task<int> GetTodayAppointmentsCountAsync()
    {
        var appointments = await _unitOfWork.Appointments.GetTodayAppointmentsAsync();
        return appointments.Count();
    }

    public async Task<int> GetUpcomingAppointmentsCountAsync()
    {
        var appointments = await _unitOfWork.Appointments.GetUpcomingAppointmentsAsync();
        return appointments.Count();
    }

    public async Task<int> GetTotalDepartmentsAsync()
    {
        var departments = await _unitOfWork.Departments.GetAllAsync();
        return departments.Count();
    }

    public async Task<IList<DTOs.AppointmentDetailDto>> GetTodayAppointmentsAsync()
    {
        var appointments = await _unitOfWork.Appointments.GetTodayAppointmentsAsync();
        return _mapper.Map<IList<DTOs.AppointmentDetailDto>>(appointments);
    }

    public async Task<IList<DTOs.AppointmentDetailDto>> GetUpcomingAppointmentsAsync()
    {
        var appointments = await _unitOfWork.Appointments.GetUpcomingAppointmentsAsync();
        return _mapper.Map<IList<DTOs.AppointmentDetailDto>>(appointments);
    }
}
