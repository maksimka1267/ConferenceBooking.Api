using ConferenceBooking.Api.DTOs.AdditionalService;
using FluentValidation;

namespace ConferenceBooking.Api.Validators;

public class UpdateAdditionalServiceRequestValidator
    : AbstractValidator<UpdateAdditionalServiceRequest>
{
    public UpdateAdditionalServiceRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Price)
            .GreaterThan(0);

        RuleFor(x => x.ConferenceHallId)
            .NotEmpty();
    }
}