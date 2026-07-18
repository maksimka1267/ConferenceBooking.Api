namespace ConferenceBooking.Api.Models.DTOs.ConferenceHall;

public class UpdateConferenceHallRequest
{
    public string Name { get; set; } = null!;

    public int Capacity { get; set; }

    public decimal HourlyRate { get; set; }
}