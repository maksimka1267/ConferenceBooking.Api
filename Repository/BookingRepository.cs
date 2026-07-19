using ConferenceBooking.Api.Data;
using ConferenceBooking.Api.Models.Entities;
using ConferenceBooking.Api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ConferenceBooking.Api.Repository;

public class BookingRepository
    : Repository<Booking>, IBookingRepository
{
    public BookingRepository(AppDbContext context)
        : base(context)
    {
    }

    public async Task<bool> HasOverlappingBookingAsync(
        Guid hallId,
        DateTime start,
        DateTime end)
    {
        return await Context.Bookings.AnyAsync(x =>
            x.ConferenceHallId == hallId &&
            start < x.EndTime &&
            end > x.StartTime);
    }
}