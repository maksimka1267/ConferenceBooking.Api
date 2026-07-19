using ConferenceBooking.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceBooking.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnalyticsController : ControllerBase
{
    private readonly IAnalyticsService _analyticsService;

    public AnalyticsController(IAnalyticsService analyticsService)
    {
        _analyticsService = analyticsService;
    }

    [HttpGet("revenue")]
    public async Task<IActionResult> GetRevenue()
    {
        var result = await _analyticsService.GetRevenueReportAsync();

        return Ok(result);
    }
    [HttpGet("popular-halls")]
    public async Task<IActionResult> GetPopularHalls()
    {
        var result = await _analyticsService.GetPopularHallsAsync();

        return Ok(result);
    }
}