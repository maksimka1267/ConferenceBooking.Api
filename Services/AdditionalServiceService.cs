using ConferenceBooking.Api.DTOs.AdditionalService;
using ConferenceBooking.Api.Models.Entities;
using ConferenceBooking.Api.Repository.Interfaces;
using ConferenceBooking.Api.Services.Interfaces;
using Mapster;

namespace ConferenceBooking.Api.Services;

public class AdditionalServiceService : IAdditionalServiceService
{
    private readonly IAdditionalServiceRepository _additionalServiceRepository;
    private readonly IConferenceHallRepository _conferenceHallRepository;

    public AdditionalServiceService(
        IAdditionalServiceRepository additionalServiceRepository,
        IConferenceHallRepository conferenceHallRepository)
    {
        _additionalServiceRepository = additionalServiceRepository;
        _conferenceHallRepository = conferenceHallRepository;
    }

    public async Task<IEnumerable<AdditionalServiceResponse>> GetAllAsync()
    {
        var services = await _additionalServiceRepository.GetAllAsync();

        return services.Adapt<IEnumerable<AdditionalServiceResponse>>();
    }

    public async Task<AdditionalServiceResponse?> GetByIdAsync(Guid id)
    {
        var service = await _additionalServiceRepository.GetByIdAsync(id);

        return service?.Adapt<AdditionalServiceResponse>();
    }

    public async Task<Guid> CreateAsync(CreateAdditionalServiceRequest request)
    {
        var hall = await _conferenceHallRepository.GetByIdAsync(request.ConferenceHallId);

        if (hall is null)
            throw new KeyNotFoundException("Conference hall was not found.");

        var service = request.Adapt<AdditionalService>();

        service.Id = Guid.NewGuid();
        service.CreatedAt = DateTime.UtcNow;
        service.UpdatedAt = DateTime.UtcNow;

        await _additionalServiceRepository.AddAsync(service);
        await _additionalServiceRepository.SaveChangesAsync();

        return service.Id;
    }

    public async Task UpdateAsync(Guid id, UpdateAdditionalServiceRequest request)
    {
        var service = await _additionalServiceRepository.GetByIdAsync(id);

        if (service is null)
            throw new KeyNotFoundException("Additional service was not found.");

        var hall = await _conferenceHallRepository.GetByIdAsync(request.ConferenceHallId);

        if (hall is null)
            throw new KeyNotFoundException("Conference hall was not found.");

        request.Adapt(service);

        service.UpdatedAt = DateTime.UtcNow;

        _additionalServiceRepository.Update(service);

        await _additionalServiceRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var service = await _additionalServiceRepository.GetByIdAsync(id);

        if (service is null)
            throw new KeyNotFoundException("Additional service was not found.");

        _additionalServiceRepository.Delete(service);

        await _additionalServiceRepository.SaveChangesAsync();
    }
}