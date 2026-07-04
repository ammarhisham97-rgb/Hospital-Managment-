using FluentValidation;
using Hospital_Managment_system.DTOs;

namespace Hospital_Managment_system.Validators;

/// <summary>
/// Validator for MedicalRecordDto.
/// </summary>
public class MedicalRecordValidator : AbstractValidator<MedicalRecordDto>
{
    public MedicalRecordValidator()
    {
        RuleFor(x => x.PatientId)
            .GreaterThan(0).WithMessage("Valid patient ID is required");

        RuleFor(x => x.DoctorId)
            .GreaterThan(0).WithMessage("Valid doctor ID is required");

        RuleFor(x => x.Diagnosis)
            .NotEmpty().WithMessage("Diagnosis is required")
            .MinimumLength(5).WithMessage("Diagnosis must be at least 5 characters")
            .MaximumLength(1000).WithMessage("Diagnosis cannot exceed 1000 characters");

        RuleFor(x => x.VisitNotes)
            .MaximumLength(1000).WithMessage("Visit notes cannot exceed 1000 characters");

        RuleFor(x => x.Symptoms)
            .MaximumLength(1000).WithMessage("Symptoms cannot exceed 1000 characters");

        RuleFor(x => x.PhysicalExamination)
            .MaximumLength(1000).WithMessage("Physical examination cannot exceed 1000 characters");

        RuleFor(x => x.LabTestResults)
            .MaximumLength(1000).WithMessage("Lab test results cannot exceed 1000 characters");

        RuleFor(x => x.TreatmentPlan)
            .MaximumLength(1000).WithMessage("Treatment plan cannot exceed 1000 characters");
    }
}

/// <summary>
/// Validator for PrescriptionDto.
/// </summary>
public class PrescriptionValidator : AbstractValidator<PrescriptionDto>
{
    public PrescriptionValidator()
    {
        RuleFor(x => x.MedicalRecordId)
            .GreaterThan(0).WithMessage("Valid medical record ID is required");

        RuleFor(x => x.MedicationName)
            .NotEmpty().WithMessage("Medication name is required")
            .MinimumLength(2).WithMessage("Medication name must be at least 2 characters")
            .MaximumLength(100).WithMessage("Medication name cannot exceed 100 characters");

        RuleFor(x => x.Dosage)
            .NotEmpty().WithMessage("Dosage is required")
            .MaximumLength(100).WithMessage("Dosage cannot exceed 100 characters");

        RuleFor(x => x.Frequency)
            .NotEmpty().WithMessage("Frequency is required")
            .MaximumLength(100).WithMessage("Frequency cannot exceed 100 characters");

        RuleFor(x => x.Duration)
            .NotEmpty().WithMessage("Duration is required")
            .MaximumLength(100).WithMessage("Duration cannot exceed 100 characters");

        RuleFor(x => x.Instructions)
            .MaximumLength(500).WithMessage("Instructions cannot exceed 500 characters");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than 0");

        RuleFor(x => x.Refills)
            .GreaterThanOrEqualTo(0).WithMessage("Refills cannot be negative");
    }
}
