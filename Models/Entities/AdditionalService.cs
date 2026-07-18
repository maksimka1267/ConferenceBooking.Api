namespace ConferenceBooking.Api.Models.Entities;

public class AdditionalService
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public Guid ConferenceHallId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public ConferenceHall ConferenceHall { get; set; } = null!;

    public ICollection<BookingService> BookingServices { get; set; } = [];
}