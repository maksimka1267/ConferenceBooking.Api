namespace ConferenceBooking.Api.Models.Analytics.DTO
{
    public class PopularHallResponse
    {
        public string HallName { get; set; } = string.Empty;

        public int BookingCount { get; set; }

        public decimal TotalRevenue { get; set; }
    }
}
