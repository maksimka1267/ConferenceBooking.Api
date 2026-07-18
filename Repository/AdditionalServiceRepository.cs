using ConferenceBooking.Api.Data;
using ConferenceBooking.Api.Models.Entities;
using ConferenceBooking.Api.Repository.Interfaces;

namespace ConferenceBooking.Api.Repository;

public class AdditionalServiceRepository
    : Repository<AdditionalService>, IAdditionalServiceRepository
{
    public AdditionalServiceRepository(AppDbContext context)
        : base(context)
    {
    }
}