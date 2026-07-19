namespace ConferenceBooking.Api.Models.Analytics.DTO
{
    public class DailyRevenueResponse
    {
        public DateTime Date { get; set; }

        public decimal TotalRevenue { get; set; }

        public int BookingCount { get; set; }
    }
}
