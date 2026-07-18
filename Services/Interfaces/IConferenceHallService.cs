using ConferenceBooking.Api.DTOs.ConferenceHall;

namespace ConferenceBooking.Api.Services.Interfaces;

public interface IConferenceHallService
{
    Task<IEnumerable<ConferenceHallResponse>> GetAllAsync();

    Task<ConferenceHallResponse?> GetByIdAsync(Guid id);

    Task<Guid> CreateAsync(CreateConferenceHallRequest request);

    Task UpdateAsync(Guid id, UpdateConferenceHallRequest request);

    Task DeleteAsync(Guid id);
}