using ConferenceBooking.Api.Models.Entities;

namespace ConferenceBooking.Api.Repository.Interfaces
{
    public interface IConferenceHallRepository : IRepository<ConferenceHall>
    {
        Task<List<ConferenceHall>> GetAvailableAsync(
            DateTime start,
            DateTime end,
            int capacity);
    }
}
