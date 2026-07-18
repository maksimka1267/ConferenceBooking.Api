using ConferenceBooking.Api.Models.Entities;

namespace ConferenceBooking.Api.Repository.Interfaces
{
    public interface IAdditionalServiceRepository:IRepository<AdditionalService>
    {
        Task<List<AdditionalService>> GetByIdsAsync(IEnumerable<Guid> ids);
    }
}
