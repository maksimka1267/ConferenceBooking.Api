namespace ConferenceBooking.Api.Models.Analytics.DTO
{
    public class DashboardResponse
    {
        public int TotalBookings { get; set; }

        public decimal TotalRevenue { get; set; }

        public decimal AverageBookingPrice { get; set; }

        public string MostPopularHall { get; set; } = string.Empty;
    }
}
