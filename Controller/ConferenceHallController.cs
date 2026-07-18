using ConferenceBooking.Api.DTOs.ConferenceHall;
using ConferenceBooking.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceBooking.Api.Controller
{
    [ApiController]
    [Route("api/conference-halls")]
    public class ConferenceHallController : ControllerBase
    {
        private readonly IConferenceHallService _conferenceHallService;

        public ConferenceHallController(IConferenceHallService conferenceHallService)
        {
            _conferenceHallService = conferenceHallService;
        }
        /// <summary>
        /// Returns all conference halls.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConferenceHallResponse>>> GetAll()
        {
            var halls = await _conferenceHallService.GetAllAsync();

            return Ok(halls);
        }

        /// <summary>
        /// Returns conference hall by identifier.
        /// </summary>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ConferenceHallResponse>> GetById(Guid id)
        {
            var hall = await _conferenceHallService.GetByIdAsync(id);

            if (hall is null)
            {
                return NotFound();
            }

            return Ok(hall);
        }

        /// <summary>
        /// Creates a new conference hall.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> Create(CreateConferenceHallRequest request)
        {
            var id = await _conferenceHallService.CreateAsync(request);

            return CreatedAtAction(
                nameof(GetById),
                new { id },
                null);
        }

        /// <summary>
        /// Updates an existing conference hall.
        /// </summary>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateConferenceHallRequest request)
        {
            await _conferenceHallService.UpdateAsync(id, request);

            return NoContent();
        }

        /// <summary>
        /// Deletes a conference hall.
        /// </summary>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _conferenceHallService.DeleteAsync(id);

            return NoContent();
        }
    }
}
