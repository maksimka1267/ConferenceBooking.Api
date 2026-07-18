using ConferenceBooking.Api.DTOs.AdditionalService;
using ConferenceBooking.Api.Models.Entities;
using Mapster;

namespace ConferenceBooking.Api.Mappings;

public static class AdditionalServiceMappingConfig
{
    public static void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AdditionalService, AdditionalServiceResponse>();

        config.NewConfig<CreateAdditionalServiceRequest, AdditionalService>();

        config.NewConfig<UpdateAdditionalServiceRequest, AdditionalService>()
            .Ignore(x => x.Id)
            .Ignore(x => x.CreatedAt)
            .Ignore(x => x.BookingServices)
            .Ignore(x => x.ConferenceHall);
    }
}