using AutoMapper;
using Hospital_Managment_system.DTOs;
using Hospital_Managment_system.Helpers;
using Hospital_Managment_system.Interfaces;
using Hospital_Managment_system.Models;
using Serilog;

namespace Hospital_Managment_system.Services;

/// <summary>
/// Service for doctor operations.
/// </summary>
public class DoctorService : IDoctorService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DoctorService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<DoctorDetailDto> CreateDoctorAsync(DoctorDto doctorDto, string userId)
    {
        var doctor = _mapper.Map<Doctor>(doctorDto);
        doctor.UserId = userId;
        doctor.CreatedAt = DateTime.UtcNow;
        doctor.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.Doctors.AddAsync(doctor);
        await _unitOfWork.SaveChangesAsync();
        Log.Information("Doctor created with ID: {DoctorId}", doctor.Id);

        return _mapper.Map<DoctorDetailDto>(await _unitOfWork.Doctors.GetWithDetailsAsync(doctor.Id));
    }

    public async Task<DoctorDetailDto?> GetDoctorAsync(int doctorId)
    {
        var doctor = await _unitOfWork.Doctors.GetWithDetailsAsync(doctorId);
        return doctor == null ? null : _mapper.Map<DoctorDetailDto>(doctor);
    }

    public async Task<PaginatedListDto<DoctorDetailDto>> GetAllDoctorsAsync(int pageNumber, int pageSize)
    {
        var (validPageNumber, validPageSize) = PaginationHelper.ValidatePageParameters(pageNumber, pageSize);
        var (doctors, totalCount) = await _unitOfWork.Doctors.GetPaginatedAsync(validPageNumber, validPageSize);
        var doctorDtos = _mapper.Map<IList<DoctorDetailDto>>(doctors);

        return new PaginatedListDto<DoctorDetailDto>
        {
            Items = doctorDtos,
            Pagination = new PaginationDto
            {
                PageNumber = validPageNumber,
                PageSize = validPageSize,
                TotalItems = totalCount,
                TotalPages = PaginationHelper.CalculateTotalPages(totalCount, validPageSize)
            }
        };
    }

    public async Task<DoctorDetailDto> UpdateDoctorAsync(int doctorId, DoctorDto doctorDto)
    {
        var doctor = await _unitOfWork.Doctors.GetByIdAsync(doctorId)
            ?? throw new KeyNotFoundException($"Doctor with ID {doctorId} not found");

        _mapper.Map(doctorDto, doctor);
        doctor.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.Doctors.UpdateAsync(doctor);
        await _unitOfWork.SaveChangesAsync();
        Log.Information("Doctor updated with ID: {DoctorId}", doctorId);

        return _mapper.Map<DoctorDetailDto>(await _unitOfWork.Doctors.GetWithDetailsAsync(doctorId));
    }

    public async Task<bool> DeleteDoctorAsync(int doctorId)
    {
        var doctor = await _unitOfWork.Doctors.GetByIdAsync(doctorId)
            ?? throw new KeyNotFoundException($"Doctor with ID {doctorId} not found");

        doctor.IsDeleted = true;
        doctor.DeletedAt = DateTime.UtcNow;

        await _unitOfWork.Doctors.UpdateAsync(doctor);
        await _unitOfWork.SaveChangesAsync();
        Log.Information("Doctor soft-deleted with ID: {DoctorId}", doctorId);
        return true;
    }

    public async Task<IList<DoctorDetailDto>> GetDoctorsBySpecializationAsync(string specialization)
    {
        var doctors = await _unitOfWork.Doctors.GetBySpecializationAsync(specialization);
        return _mapper.Map<IList<DoctorDetailDto>>(doctors);
    }

    public async Task<IList<DoctorDetailDto>> GetDoctorsByDepartmentAsync(int departmentId)
    {
        var doctors = await _unitOfWork.Doctors.GetByDepartmentAsync(departmentId);
        return _mapper.Map<IList<DoctorDetailDto>>(doctors);
    }

    public async Task<IList<DoctorDetailDto>> GetAvailableDoctorsAsync()
    {
        var doctors = await _unitOfWork.Doctors.GetAvailableAsync();
        return _mapper.Map<IList<DoctorDetailDto>>(doctors);
    }

    public async Task<IList<DoctorDetailDto>> SearchDoctorsAsync(string searchTerm)
    {
        var doctors = await _unitOfWork.Doctors.SearchAsync(searchTerm);
        return _mapper.Map<IList<DoctorDetailDto>>(doctors);
    }

    public async Task<IList<DoctorScheduleDto>> GetDoctorScheduleAsync(int doctorId)
    {
        var doctor = await _unitOfWork.Doctors.GetWithScheduleAsync(doctorId)
            ?? throw new KeyNotFoundException($"Doctor with ID {doctorId} not found");

        return _mapper.Map<IList<DoctorScheduleDto>>(doctor.Schedules);
    }

    public async Task<DoctorScheduleDto> AddDoctorScheduleAsync(int doctorId, DoctorScheduleDto scheduleDto)
    {
        var schedule = _mapper.Map<DoctorSchedule>(scheduleDto);
        schedule.DoctorId = doctorId;
        schedule.CreatedAt = DateTime.UtcNow;
        schedule.UpdatedAt = DateTime.UtcNow;

        var repo = _unitOfWork.Repository<DoctorSchedule>();
        await repo.AddAsync(schedule);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<DoctorScheduleDto>(schedule);
    }

    public async Task<DoctorScheduleDto> UpdateDoctorScheduleAsync(int scheduleId, DoctorScheduleDto scheduleDto)
    {
        var repo = _unitOfWork.Repository<DoctorSchedule>();
        var schedule = await repo.GetByIdAsync(scheduleId)
            ?? throw new KeyNotFoundException($"Schedule with ID {scheduleId} not found");

        _mapper.Map(scheduleDto, schedule);
        schedule.UpdatedAt = DateTime.UtcNow;

        await repo.UpdateAsync(schedule);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<DoctorScheduleDto>(schedule);
    }

    public async Task<bool> DeleteDoctorScheduleAsync(int scheduleId)
    {
        var repo = _unitOfWork.Repository<DoctorSchedule>();
        await repo.DeleteAsync(scheduleId);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
