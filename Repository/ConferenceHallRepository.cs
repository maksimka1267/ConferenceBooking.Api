using ConferenceBooking.Api.Data;
using ConferenceBooking.Api.Models.Entities;
using ConferenceBooking.Api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ConferenceBooking.Api.Repository;

public class ConferenceHallRepository
    : Repository<ConferenceHall>, IConferenceHallRepository
{
    public ConferenceHallRepository(AppDbContext context)
        : base(context)
    {
    }

    public async Task<List<ConferenceHall>> GetAvailableAsync(
        DateTime start,
        DateTime end,
        int capacity)
    {
        return await Context.ConferenceHalls
            .AsNoTracking()
            .Include(x => x.AdditionalServices)
            .Where(x =>
                x.Capacity >= capacity &&
                !x.Bookings.Any(b =>
                    start < b.EndTime &&
                    end > b.StartTime))
            .ToListAsync();
    }
}