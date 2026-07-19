using ConferenceBooking.Api.DTOs.Booking;
using ConferenceBooking.Api.Models.Entities;
using ConferenceBooking.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceBooking.Api.Controllers;

[ApiController]
[Route("api/bookings")]
public class BookingsController : ControllerBase
{
    private readonly IBookingManagementService _bookingService;

    public BookingsController(IBookingManagementService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpGet("available")]
    public async Task<ActionResult<IEnumerable<AvailableConferenceHallResponse>>> GetAvailable(
        [FromQuery] DateTime start,
        [FromQuery] DateTime end,
        [FromQuery] int capacity)
    {
        var halls = await _bookingService.GetAvailableAsync(
            start,
            end,
            capacity);

        return Ok(halls);
    }

    [HttpPost]
    public async Task<ActionResult<BookingResponse>> Create(
        CreateBookingRequest request)
    {
        var booking = await _bookingService.CreateAsync(request);

        return Ok(booking);
    }
}