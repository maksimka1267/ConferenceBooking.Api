using ConferenceBooking.Api.Data;
using ConferenceBooking.Api.Models.Entities;
using ConferenceBooking.Api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ConferenceBooking.Api.Repository;

public class AdditionalServiceRepository
    : Repository<AdditionalService>, IAdditionalServiceRepository
{
    public AdditionalServiceRepository(AppDbContext context)
        : base(context)
    {
    }
    public async Task<List<AdditionalService>> GetByIdsAsync(IEnumerable<Guid> ids)
    {
        return await Context.AdditionalServices
            .Where(x => ids.Contains(x.Id))
            .ToListAsync();
    }
}