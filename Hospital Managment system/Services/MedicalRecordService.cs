using AutoMapper;
using Hospital_Managment_system.DTOs;
using Hospital_Managment_system.Helpers;
using Hospital_Managment_system.Interfaces;
using Hospital_Managment_system.Models;
using Serilog;

namespace Hospital_Managment_system.Services;

/// <summary>
/// Service for medical record operations.
/// </summary>
public class MedicalRecordService : IMedicalRecordService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public MedicalRecordService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<MedicalRecordDetailDto> CreateMedicalRecordAsync(MedicalRecordDto medicalRecordDto)
    {
        var record = _mapper.Map<MedicalRecord>(medicalRecordDto);
        record.CreatedAt = DateTime.UtcNow;
        record.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.MedicalRecords.AddAsync(record);
        await _unitOfWork.SaveChangesAsync();

        Log.Information("Medical record created with ID: {RecordId}", record.Id);
        return _mapper.Map<MedicalRecordDetailDto>(await _unitOfWork.MedicalRecords.GetWithPrescriptionsAsync(record.Id));
    }

    public async Task<MedicalRecordDetailDto?> GetMedicalRecordAsync(int recordId)
    {
        var record = await _unitOfWork.MedicalRecords.GetWithPrescriptionsAsync(recordId);
        return record == null ? null : _mapper.Map<MedicalRecordDetailDto>(record);
    }

    public async Task<PaginatedListDto<MedicalRecordDetailDto>> GetAllMedicalRecordsAsync(int pageNumber, int pageSize)
    {
        var (validPageNumber, validPageSize) = PaginationHelper.ValidatePageParameters(pageNumber, pageSize);
        var (records, totalCount) = await _unitOfWork.MedicalRecords.GetPaginatedAsync(validPageNumber, validPageSize);
        var recordDtos = _mapper.Map<IList<MedicalRecordDetailDto>>(records);

        return new PaginatedListDto<MedicalRecordDetailDto>
        {
            Items = recordDtos,
            Pagination = new PaginationDto
            {
                PageNumber = validPageNumber,
                PageSize = validPageSize,
                TotalItems = totalCount,
                TotalPages = PaginationHelper.CalculateTotalPages(totalCount, validPageSize)
            }
        };
    }

    public async Task<MedicalRecordDetailDto> UpdateMedicalRecordAsync(int recordId, MedicalRecordDto medicalRecordDto)
    {
        var record = await _unitOfWork.MedicalRecords.GetByIdAsync(recordId)
            ?? throw new KeyNotFoundException($"Medical record with ID {recordId} not found");

        _mapper.Map(medicalRecordDto, record);
        record.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.MedicalRecords.UpdateAsync(record);
        await _unitOfWork.SaveChangesAsync();

        Log.Information("Medical record updated with ID: {RecordId}", recordId);
        return _mapper.Map<MedicalRecordDetailDto>(await _unitOfWork.MedicalRecords.GetWithPrescriptionsAsync(recordId));
    }

    public async Task<bool> DeleteMedicalRecordAsync(int recordId)
    {
        var record = await _unitOfWork.MedicalRecords.GetByIdAsync(recordId)
            ?? throw new KeyNotFoundException($"Medical record with ID {recordId} not found");

        record.IsDeleted = true;
        record.DeletedAt = DateTime.UtcNow;

        await _unitOfWork.MedicalRecords.UpdateAsync(record);
        await _unitOfWork.SaveChangesAsync();

        Log.Information("Medical record soft-deleted with ID: {RecordId}", recordId);
        return true;
    }

    public async Task<IList<MedicalRecordDetailDto>> GetPatientMedicalRecordsAsync(int patientId)
    {
        var records = await _unitOfWork.MedicalRecords.GetPatientRecordsAsync(patientId);
        return _mapper.Map<IList<MedicalRecordDetailDto>>(records);
    }

    public async Task<IList<MedicalRecordDetailDto>> GetDoctorMedicalRecordsAsync(int doctorId)
    {
        var records = await _unitOfWork.MedicalRecords.GetDoctorRecordsAsync(doctorId);
        return _mapper.Map<IList<MedicalRecordDetailDto>>(records);
    }

    public async Task<PrescriptionDetailDto> AddPrescriptionAsync(int recordId, PrescriptionDto prescriptionDto)
    {
        var prescription = _mapper.Map<Prescription>(prescriptionDto);
        prescription.MedicalRecordId = recordId;
        prescription.CreatedAt = DateTime.UtcNow;
        prescription.UpdatedAt = DateTime.UtcNow;

        var repo = _unitOfWork.Repository<Prescription>();
        await repo.AddAsync(prescription);
        await _unitOfWork.SaveChangesAsync();

        Log.Information("Prescription added with ID: {PrescriptionId}", prescription.Id);
        return _mapper.Map<PrescriptionDetailDto>(prescription);
    }

    public async Task<IList<PrescriptionDetailDto>> GetPrescriptionsAsync(int recordId)
    {
        var record = await _unitOfWork.MedicalRecords.GetWithPrescriptionsAsync(recordId)
            ?? throw new KeyNotFoundException($"Medical record with ID {recordId} not found");

        return _mapper.Map<IList<PrescriptionDetailDto>>(record.Prescriptions);
    }

    public async Task<PrescriptionDetailDto> UpdatePrescriptionAsync(int prescriptionId, PrescriptionDto prescriptionDto)
    {
        var repo = _unitOfWork.Repository<Prescription>();
        var prescription = await repo.GetByIdAsync(prescriptionId)
            ?? throw new KeyNotFoundException($"Prescription with ID {prescriptionId} not found");

        _mapper.Map(prescriptionDto, prescription);
        prescription.UpdatedAt = DateTime.UtcNow;

        await repo.UpdateAsync(prescription);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<PrescriptionDetailDto>(prescription);
    }

    public async Task<bool> DeletePrescriptionAsync(int prescriptionId)
    {
        var repo = _unitOfWork.Repository<Prescription>();
        await repo.DeleteAsync(prescriptionId);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
