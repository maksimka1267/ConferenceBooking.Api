using ConferenceBooking.Api.DTOs.AdditionalService;

namespace ConferenceBooking.Api.Services.Interfaces;

public interface IAdditionalServiceService
{
    Task<IEnumerable<AdditionalServiceResponse>> GetAllAsync();

    Task<AdditionalServiceResponse?> GetByIdAsync(Guid id);

    Task<Guid> CreateAsync(CreateAdditionalServiceRequest request);

    Task UpdateAsync(Guid id, UpdateAdditionalServiceRequest request);

    Task DeleteAsync(Guid id);
}