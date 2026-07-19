using ConferenceBooking.Api.DTOs.Booking;

namespace ConferenceBooking.Api.Services.Interfaces;

public interface IBookingManagementService
{
    Task<BookingResponse> CreateAsync(CreateBookingRequest request);

    Task<IEnumerable<AvailableConferenceHallResponse>> GetAvailableAsync(
        DateTime start,
        DateTime end,
        int capacity);
}