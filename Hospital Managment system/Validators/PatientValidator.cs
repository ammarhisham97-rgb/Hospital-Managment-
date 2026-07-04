using FluentValidation;
using Hospital_Managment_system.DTOs;

namespace Hospital_Managment_system.Validators;

/// <summary>
/// Validator for PatientDto.
/// </summary>
public class PatientValidator : AbstractValidator<PatientDto>
{
    public PatientValidator()
    {
        RuleFor(x => x.BloodGroup)
            .Must(bg => IsValidBloodGroup(bg))
            .WithMessage("Invalid blood group");

        RuleFor(x => x.Allergies)
            .MaximumLength(500).WithMessage("Allergies cannot exceed 500 characters");

        RuleFor(x => x.MedicalHistory)
            .MaximumLength(1000).WithMessage("Medical history cannot exceed 1000 characters");

        RuleFor(x => x.EmergencyContactName)
            .MaximumLength(100).WithMessage("Emergency contact name cannot exceed 100 characters");

        RuleFor(x => x.EmergencyContactPhone)
            .Matches(@"^\+?1?\d{9,15}$").WithMessage("Emergency contact number format is invalid")
            .When(x => !string.IsNullOrEmpty(x.EmergencyContactPhone));

        RuleFor(x => x.InsuranceProvider)
            .MaximumLength(100).WithMessage("Insurance provider cannot exceed 100 characters");

        RuleFor(x => x.InsurancePolicyNumber)
            .MaximumLength(50).WithMessage("Insurance policy number cannot exceed 50 characters");
    }

    private static bool IsValidBloodGroup(string? bloodGroup)
    {
        if (string.IsNullOrEmpty(bloodGroup))
            return true;

        var validGroups = new[] { "O+", "O-", "A+", "A-", "B+", "B-", "AB+", "AB-" };
        return validGroups.Contains(bloodGroup);
    }
}
