using ConferenceBooking.Api.Data;
using ConferenceBooking.Api.Models.Entities;
using ConferenceBooking.Api.Repository.Interfaces;

namespace ConferenceBooking.Api.Repository;

public class BookingServiceRepository
    : Repository<BookingService>, IBookingServiceRepository
{
    public BookingServiceRepository(AppDbContext context)
        : base(context)
    {
    }
}