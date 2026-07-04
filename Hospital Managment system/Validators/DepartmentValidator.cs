using FluentValidation;
using Hospital_Managment_system.DTOs;

namespace Hospital_Managment_system.Validators;

/// <summary>
/// Validator for DepartmentDto.
/// </summary>
public class DepartmentValidator : AbstractValidator<DepartmentDto>
{
    public DepartmentValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Department name is required")
            .MinimumLength(2).WithMessage("Department name must be at least 2 characters")
            .MaximumLength(100).WithMessage("Department name cannot exceed 100 characters");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters");

        RuleFor(x => x.ContactNumber)
            .Matches(@"^\+?1?\d{9,15}$").WithMessage("Contact number format is invalid")
            .When(x => !string.IsNullOrEmpty(x.ContactNumber));

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Email format is invalid")
            .When(x => !string.IsNullOrEmpty(x.Email));
    }
}
