using ConferenceBooking.Api.DTOs.Analytics;
using ConferenceBooking.Api.Models.Analytics.DTO;

namespace ConferenceBooking.Api.Services.Interfaces
{
    public interface IAnalyticsService
    {
        Task<RevenueReportResponse> GetRevenueReportAsync();
        Task<IEnumerable<PopularHallResponse>> GetPopularHallsAsync();
        Task<IEnumerable<DailyRevenueResponse>> GetDailyRevenueAsync();
        Task<IEnumerable<HallUtilizationResponse>> GetHallUtilizationAsync();
        Task<DashboardResponse> GetDashboardAsync();
    }
}
