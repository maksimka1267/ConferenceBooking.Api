using ConferenceBooking.Api.DTOs.ConferenceHall;
using FluentValidation;

namespace ConferenceBooking.Api.Validators.ConferenceHall
{
    public class UpdateConferenceHallRequestValidator
    : AbstractValidator<UpdateConferenceHallRequest>
    {
        public UpdateConferenceHallRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Conference hall name is required.")
                .MaximumLength(100)
                .WithMessage("Conference hall name must not exceed 100 characters.");

            RuleFor(x => x.Capacity)
                .GreaterThan(0)
                .WithMessage("Capacity must be greater than zero.");

            RuleFor(x => x.HourlyRate)
                .GreaterThan(0)
                .WithMessage("Hourly rate must be greater than zero.");
        }
    }
}
