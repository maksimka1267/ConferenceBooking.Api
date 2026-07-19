using ConferenceBooking.Api.DTOs.Booking;
using ConferenceBooking.Api.Models.Entities;
using Mapster;

namespace ConferenceBooking.Api.Mappings;

public static class BookingMappingConfig
{
    public static void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Booking, BookingResponse>()
            .Map(dest => dest.ServiceIds,
                 src => src.BookedServices.Select(x => x.ServiceId));

        config.NewConfig<CreateBookingRequest, Booking>()
            .Ignore(x => x.Id)
            .Ignore(x => x.TotalPrice)
            .Ignore(x => x.CreatedAt)
            .Ignore(x => x.UpdatedAt)
            .Ignore(x => x.ConferenceHall)
            .Ignore(x => x.BookedServices);

        config.NewConfig<ConferenceHall, AvailableConferenceHallResponse>();
    }
}