using Mapster;

namespace ConferenceBooking.Api.Mappings;

public static class MapsterConfig
{
    public static void RegisterMappings()
    {
        var config = TypeAdapterConfig.GlobalSettings;

        ConferenceHallMappingConfig.Register(config);
        AdditionalServiceMappingConfig.Register(config);
        BookingMappingConfig.Register(config);
    }
}