using ConferenceBooking.Api.DTOs.Booking;
using FluentValidation;

namespace ConferenceBooking.Api.Validators;

public class CreateBookingRequestValidator
    : AbstractValidator<CreateBookingRequest>
{
    public CreateBookingRequestValidator()
    {
        RuleFor(x => x.ConferenceHallId)
            .NotEmpty();

        RuleFor(x => x.StartTime)
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("Start time must be in the future.");

        RuleFor(x => x.EndTime)
            .GreaterThan(x => x.StartTime)
            .WithMessage("End time must be greater than start time.");

        RuleFor(x => x.ServiceIds)
            .NotNull();
        RuleFor(x => x.StartTime.Minute)
            .Equal(0);

        RuleFor(x => x.EndTime.Minute)
            .Equal(0);
        RuleFor(x => x.StartTime.TimeOfDay)
            .GreaterThanOrEqualTo(TimeSpan.FromHours(6))
            .LessThan(TimeSpan.FromHours(23));

        RuleFor(x => x.EndTime.TimeOfDay)
            .GreaterThan(TimeSpan.FromHours(6))
            .LessThanOrEqualTo(TimeSpan.FromHours(23));
    }
}