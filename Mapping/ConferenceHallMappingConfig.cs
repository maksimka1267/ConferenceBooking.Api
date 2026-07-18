using ConferenceBooking.Api.Models.DTOs.ConferenceHall;
using ConferenceBooking.Api.Models.Entities;
using Mapster;

namespace ConferenceBooking.Api.Mappings;

public static class ConferenceHallMappingConfig
{
    public static void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ConferenceHall, ConferenceHallResponse>();

        config.NewConfig<CreateConferenceHallRequest, ConferenceHall>();

        config.NewConfig<UpdateConferenceHallRequest, ConferenceHall>();
    }
}