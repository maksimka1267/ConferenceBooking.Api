namespace ConferenceBooking.Api.DTOs.AdditionalService;

public class CreateAdditionalServiceRequest
{
    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public Guid ConferenceHallId { get; set; }
}