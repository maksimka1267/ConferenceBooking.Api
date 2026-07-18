namespace ConferenceBooking.Api.Models.Entities;

public class Booking
{
    public Guid Id { get; set; }

    public Guid ConferenceHallId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public decimal TotalPrice { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public ConferenceHall ConferenceHall { get; set; } = null!;

    public ICollection<BookingService> BookedServices { get; set; } = [];
}