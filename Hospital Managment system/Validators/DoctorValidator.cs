using FluentValidation;
using Hospital_Managment_system.DTOs;

namespace Hospital_Managment_system.Validators;

/// <summary>
/// Validator for DoctorDto.
/// </summary>
public class DoctorValidator : AbstractValidator<DoctorDto>
{
    public DoctorValidator()
    {
        RuleFor(x => x.LicenseNumber)
            .NotEmpty().WithMessage("License number is required")
            .MinimumLength(5).WithMessage("License number must be at least 5 characters");

        RuleFor(x => x.Specialization)
            .NotEmpty().WithMessage("Specialization is required")
            .MinimumLength(3).WithMessage("Specialization must be at least 3 characters");

        RuleFor(x => x.YearsOfExperience)
            .GreaterThanOrEqualTo(0).WithMessage("Years of experience cannot be negative")
            .LessThanOrEqualTo(70).WithMessage("Years of experience cannot exceed 70");

        RuleFor(x => x.DepartmentId)
            .GreaterThan(0).WithMessage("Valid department ID is required");

        RuleFor(x => x.ConsultationFee)
            .GreaterThan(0).WithMessage("Consultation fee must be greater than 0");

        RuleFor(x => x.Qualifications)
            .MaximumLength(500).WithMessage("Qualifications cannot exceed 500 characters");
    }
}
