namespace ConferenceBooking.Api.Models.Entities;

public class BookingService
{
    public Guid BookingId { get; set; }

    public Guid ServiceId { get; set; }

    public decimal Price { get; set; }

    public Booking Booking { get; set; } = null!;

    public AdditionalService Service { get; set; } = null!;
}