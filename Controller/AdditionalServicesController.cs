using ConferenceBooking.Api.DTOs.AdditionalService;
using ConferenceBooking.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceBooking.Api.Controllers;

[ApiController]
[Route("api/additional-services")]
public class AdditionalServicesController : ControllerBase
{
    private readonly IAdditionalServiceService _additionalServiceService;

    public AdditionalServicesController(IAdditionalServiceService additionalServiceService)
    {
        _additionalServiceService = additionalServiceService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AdditionalServiceResponse>>> GetAll()
    {
        var services = await _additionalServiceService.GetAllAsync();

        return Ok(services);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<AdditionalServiceResponse>> GetById(Guid id)
    {
        var service = await _additionalServiceService.GetByIdAsync(id);

        if (service is null)
            return NotFound();

        return Ok(service);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateAdditionalServiceRequest request)
    {
        var id = await _additionalServiceService.CreateAsync(request);

        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateAdditionalServiceRequest request)
    {
        await _additionalServiceService.UpdateAsync(id, request);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _additionalServiceService.DeleteAsync(id);

        return NoContent();
    }
}