using ConferenceBooking.Api.DTOs.Analytics;

namespace ConferenceBooking.Api.Services.Interfaces
{
    public interface IAnalyticsService
    {
        Task<RevenueReportResponse> GetRevenueReportAsync();
    }
}
