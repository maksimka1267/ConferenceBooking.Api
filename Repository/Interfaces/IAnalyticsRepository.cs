using ConferenceBooking.Api.DTOs.Analytics;
using ConferenceBooking.Api.Models.Analytics;
using ConferenceBooking.Api.Models.Analytics.DTO;

namespace ConferenceBooking.Api.Repository.Interfaces;

public interface IAnalyticsRepository
{
    Task SaveBookingAsync(BookingAnalytics booking);
    Task<RevenueReportResponse> GetRevenueReportAsync();
    Task<IEnumerable<PopularHallResponse>> GetPopularHallsAsync();
}