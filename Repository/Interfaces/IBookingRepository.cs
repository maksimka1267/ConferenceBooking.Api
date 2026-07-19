using ConferenceBooking.Api.Models.Entities;

namespace ConferenceBooking.Api.Repository.Interfaces
{
    public interface IBookingRepository : IRepository<Booking>
    {
        Task<bool> HasOverlappingBookingAsync(
            Guid hallId,
            DateTime start,
            DateTime end);
    }
}
