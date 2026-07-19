namespace ConferenceBooking.Api.Models.Analytics
{
    public class BookingAnalytics
    {
        public Guid BookingId { get; set; }

        public Guid HallId { get; set; }

        public string HallName { get; set; } = string.Empty;

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public decimal TotalPrice { get; set; }

        public int ServiceCount { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
