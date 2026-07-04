using FluentValidation;
using Hospital_Managment_system.DTOs;

namespace Hospital_Managment_system.Validators;

/// <summary>
/// Validator for AppointmentDto.
/// </summary>
public class AppointmentValidator : AbstractValidator<AppointmentDto>
{
    public AppointmentValidator()
    {
        RuleFor(x => x.PatientId)
            .GreaterThan(0).WithMessage("Valid patient ID is required");

        RuleFor(x => x.DoctorId)
            .GreaterThan(0).WithMessage("Valid doctor ID is required");

        RuleFor(x => x.AppointmentDateTime)
            .GreaterThan(DateTime.UtcNow).WithMessage("Appointment date must be in the future");

        RuleFor(x => x.DurationInMinutes)
            .GreaterThan(0).WithMessage("Duration must be greater than 0")
            .LessThanOrEqualTo(480).WithMessage("Duration cannot exceed 8 hours");

        RuleFor(x => x.ReasonForVisit)
            .MaximumLength(500).WithMessage("Reason for visit cannot exceed 500 characters");

        RuleFor(x => x.AppointmentType)
            .MaximumLength(50).WithMessage("Appointment type cannot exceed 50 characters");
    }
}

/// <summary>
/// Validator for RescheduleAppointmentDto.
/// </summary>
public class RescheduleAppointmentValidator : AbstractValidator<RescheduleAppointmentDto>
{
    public RescheduleAppointmentValidator()
    {
        RuleFor(x => x.NewAppointmentDateTime)
            .GreaterThan(DateTime.UtcNow).WithMessage("New appointment date must be in the future");

        RuleFor(x => x.Reason)
            .MaximumLength(500).WithMessage("Reason cannot exceed 500 characters");
    }
}

/// <summary>
/// Validator for CancelAppointmentDto.
/// </summary>
public class CancelAppointmentValidator : AbstractValidator<CancelAppointmentDto>
{
    public CancelAppointmentValidator()
    {
        RuleFor(x => x.Reason)
            .MaximumLength(500).WithMessage("Reason cannot exceed 500 characters");
    }
}
