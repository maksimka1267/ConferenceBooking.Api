namespace ConferenceBooking.Api.DTOs.Analytics;

public class RevenueReportResponse
{
    public decimal TotalRevenue { get; set; }

    public int BookingCount { get; set; }

    public decimal AverageBookingPrice { get; set; }
}