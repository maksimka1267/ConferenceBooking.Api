namespace ConferenceBooking.Api.Models.Analytics.DTO
{
    public class HallUtilizationResponse
    {
        public string HallName { get; set; } = string.Empty;

        public int BookingCount { get; set; }

        public double TotalBookedHours { get; set; }

        public double AverageBookingDuration { get; set; }
    }
}
