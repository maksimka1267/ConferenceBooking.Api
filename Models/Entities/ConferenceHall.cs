namespace ConferenceBooking.Api.Models.Entities;

public class ConferenceHall
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public int Capacity { get; set; }

    public decimal HourlyRate { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public ICollection<AdditionalService> AdditionalServices { get; set; } = [];
    public ICollection<Booking> Bookings { get; set; } = [];
}