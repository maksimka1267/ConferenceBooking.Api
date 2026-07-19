namespace ConferenceBooking.Api.Models.DTOs.ConferenceHall;

public class CreateConferenceHallRequest
{
    public string Name { get; set; } = null!;

    public int Capacity { get; set; }

    public decimal HourlyRate { get; set; }
}