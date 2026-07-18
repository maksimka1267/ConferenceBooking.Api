namespace ConferenceBooking.Api.DTOs.Booking;

public class BookingResponse
{
    public Guid Id { get; set; }

    public Guid ConferenceHallId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public decimal TotalPrice { get; set; }

    public List<Guid> ServiceIds { get; set; } = [];
}