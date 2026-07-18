namespace ConferenceBooking.Api.DTOs.AdditionalService;

public class AdditionalServiceResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public Guid ConferenceHallId { get; set; }
}