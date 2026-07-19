using ConferenceBooking.Api.DTOs.Analytics;
using ConferenceBooking.Api.Models.Analytics.DTO;
using ConferenceBooking.Api.Repository.Interfaces;
using ConferenceBooking.Api.Services.Interfaces;

namespace ConferenceBooking.Api.Services
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly IAnalyticsRepository _analyticsRepository;

        public AnalyticsService(IAnalyticsRepository analyticsRepository)
        {
            _analyticsRepository = analyticsRepository;
        }

        public async Task<RevenueReportResponse> GetRevenueReportAsync()
        {
            return await _analyticsRepository.GetRevenueReportAsync();
        }
        public async Task<IEnumerable<PopularHallResponse>> GetPopularHallsAsync()
        {
            return await _analyticsRepository.GetPopularHallsAsync();
        }
        public async Task<IEnumerable<DailyRevenueResponse>> GetDailyRevenueAsync()
        {
            return await _analyticsRepository.GetDailyRevenueAsync();
        }
    }
}
