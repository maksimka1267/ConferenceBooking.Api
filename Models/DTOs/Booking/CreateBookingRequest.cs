namespace ConferenceBooking.Api.DTOs.Booking;

public class CreateBookingRequest
{
    public Guid ConferenceHallId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public List<Guid> ServiceIds { get; set; } = [];
}