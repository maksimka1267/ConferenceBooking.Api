using ConferenceBooking.Api.DTOs.ConferenceHall;
using ConferenceBooking.Api.Models.Entities;
using ConferenceBooking.Api.Repository.Interfaces;
using ConferenceBooking.Api.Services.Interfaces;
using Mapster;

namespace ConferenceBooking.Api.Services;

public class ConferenceHallService : IConferenceHallService
{
    private readonly IConferenceHallRepository _conferenceHallRepository;

    public ConferenceHallService(IConferenceHallRepository conferenceHallRepository)
    {
        _conferenceHallRepository = conferenceHallRepository;
    }

    public async Task<IEnumerable<ConferenceHallResponse>> GetAllAsync()
    {
        var halls = await _conferenceHallRepository.GetAllAsync();

        return halls.Adapt<IEnumerable<ConferenceHallResponse>>();
    }

    public async Task<ConferenceHallResponse?> GetByIdAsync(Guid id)
    {
        var hall = await _conferenceHallRepository.GetByIdAsync(id);

        return hall?.Adapt<ConferenceHallResponse>();
    }

    public async Task<Guid> CreateAsync(CreateConferenceHallRequest request)
    {
        var hall = request.Adapt<ConferenceHall>();

        hall.Id = Guid.NewGuid();
        hall.CreatedAt = DateTime.UtcNow;
        hall.UpdatedAt = DateTime.UtcNow;

        await _conferenceHallRepository.AddAsync(hall);
        await _conferenceHallRepository.SaveChangesAsync();

        return hall.Id;
    }

    public async Task UpdateAsync(Guid id, UpdateConferenceHallRequest request)
    {
        var hall = await _conferenceHallRepository.GetByIdAsync(id);

        if (hall is null)
            throw new KeyNotFoundException($"Conference hall with id '{id}' was not found.");

        request.Adapt(hall);

        hall.UpdatedAt = DateTime.UtcNow;

        _conferenceHallRepository.Update(hall);

        await _conferenceHallRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var hall = await _conferenceHallRepository.GetByIdAsync(id);

        if (hall is null)
            throw new KeyNotFoundException($"Conference hall with id '{id}' was not found.");

        _conferenceHallRepository.Delete(hall);

        await _conferenceHallRepository.SaveChangesAsync();
    }
}