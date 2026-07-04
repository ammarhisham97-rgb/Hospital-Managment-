using AutoMapper;
using Hospital_Managment_system.DTOs;
using Hospital_Managment_system.Helpers;
using Hospital_Managment_system.Interfaces;
using Hospital_Managment_system.Models;
using Serilog;

namespace Hospital_Managment_system.Services;

/// <summary>
/// Service for patient operations.
/// </summary>
public class PatientService : IPatientService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PatientService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PatientDetailDto> CreatePatientAsync(PatientDto patientDto, string userId)
    {
        var patient = _mapper.Map<Patient>(patientDto);
        patient.UserId = userId;
        patient.PatientNumber = $"PT-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper()}";
        patient.CreatedAt = DateTime.UtcNow;
        patient.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.Patients.AddAsync(patient);
        await _unitOfWork.SaveChangesAsync();

        Log.Information("Patient created with ID: {PatientId}", patient.Id);

        return _mapper.Map<PatientDetailDto>(await _unitOfWork.Patients.GetWithDetailsAsync(patient.Id));
    }

    public async Task<PatientDetailDto?> GetPatientAsync(int patientId)
    {
        var patient = await _unitOfWork.Patients.GetWithDetailsAsync(patientId);
        return patient == null ? null : _mapper.Map<PatientDetailDto>(patient);
    }

    public async Task<PaginatedListDto<PatientDetailDto>> GetAllPatientsAsync(int pageNumber, int pageSize)
    {
        var (validPageNumber, validPageSize) = PaginationHelper.ValidatePageParameters(pageNumber, pageSize);

        var (patients, totalCount) = await _unitOfWork.Patients.GetPaginatedAsync(validPageNumber, validPageSize);
        var patientDtos = _mapper.Map<IList<PatientDetailDto>>(patients);

        return new PaginatedListDto<PatientDetailDto>
        {
            Items = patientDtos,
            Pagination = new PaginationDto
            {
                PageNumber = validPageNumber,
                PageSize = validPageSize,
                TotalItems = totalCount,
                TotalPages = PaginationHelper.CalculateTotalPages(totalCount, validPageSize)
            }
        };
    }

    public async Task<PatientDetailDto> UpdatePatientAsync(int patientId, PatientDto patientDto)
    {
        var patient = await _unitOfWork.Patients.GetByIdAsync(patientId)
            ?? throw new KeyNotFoundException($"Patient with ID {patientId} not found");

        _mapper.Map(patientDto, patient);
        patient.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.Patients.UpdateAsync(patient);
        await _unitOfWork.SaveChangesAsync();

        Log.Information("Patient updated with ID: {PatientId}", patientId);

        return _mapper.Map<PatientDetailDto>(await _unitOfWork.Patients.GetWithDetailsAsync(patientId));
    }

    public async Task<bool> DeletePatientAsync(int patientId)
    {
        var patient = await _unitOfWork.Patients.GetByIdAsync(patientId)
            ?? throw new KeyNotFoundException($"Patient with ID {patientId} not found");

        patient.IsDeleted = true;
        patient.DeletedAt = DateTime.UtcNow;

        await _unitOfWork.Patients.UpdateAsync(patient);
        await _unitOfWork.SaveChangesAsync();

        Log.Information("Patient soft-deleted with ID: {PatientId}", patientId);
        return true;
    }

    public async Task<IList<PatientSearchDto>> SearchPatientsAsync(string searchTerm)
    {
        var patients = await _unitOfWork.Patients.SearchAsync(searchTerm);
        return _mapper.Map<IList<PatientSearchDto>>(patients);
    }

    public async Task<IList<MedicalRecordDetailDto>> GetPatientMedicalHistoryAsync(int patientId)
    {
        var records = await _unitOfWork.MedicalRecords.GetPatientRecordsAsync(patientId);
        return _mapper.Map<IList<MedicalRecordDetailDto>>(records);
    }
}
