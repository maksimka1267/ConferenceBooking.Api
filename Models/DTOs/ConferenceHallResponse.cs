namespace ConferenceBooking.Api.DTOs.ConferenceHall;

public class ConferenceHallResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public int Capacity { get; set; }

    public decimal HourlyRate { get; set; }
}