using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BorrowBuddy.Dto;
using BorrowBuddy.Models;
using BorrowBuddy.Models.Requests;
using BorrowBuddy.Models.Resources;
using BorrowBuddy.Services;
using Microsoft.AspNetCore.Mvc;

namespace BorrowBuddy.Controllers {
  [ApiController]
  [Route("api/[controller]")]
  public class ParticipantsController : ControllerBase {
    private readonly ParticipantService _participantService;
    private readonly BalanceService _balanceService;

    public ParticipantsController(ParticipantService participantService, BalanceService balanceService) {
      _balanceService = balanceService;
      _participantService = participantService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Participant>>> GetParticipantAsync() {
      return (await _participantService.GetAsync()).Select(Mapper.Map).ToList();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Participant>> GetParticipant(Guid id) {
      var participant = await _participantService.GetAsync(id);

      if(participant == null) {
        return NotFound();
      }

      return Mapper.Map(participant);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutParticipant([FromRoute] Guid id, [FromBody] Participant model) {
      if(model.Id != id) {
        return BadRequest();
      }

      if(!await ParticipantExistsAsync(id)) {
        return NotFound();
      }

      var flow = await _participantService.UpdateAsync(id,
        new ParticipantDto {
          FirstName = model.FirstName,
          LastName = model.LastName,
          MiddleName = model.MiddleName
        });

      return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<ActionResult<Participant>> PostParticipant(ParticipantPost model) {
      var participant = await _participantService.AddAsync(new ParticipantDto {
        FirstName = model.FirstName,
        LastName = model.LastName,
        MiddleName = model.MiddleName
      });

      return CreatedAtAction(nameof(GetParticipant), new { id = participant.Id }, Mapper.Map(participant));
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteParticipant(Guid id) {
      if(!await ParticipantExistsAsync(id)) {
        return NotFound();
      }

      await _participantService.DeleteAsync(id);
      return NoContent();
    }

        [HttpGet("{from}/balance/{to}")]
    public async Task<ActionResult<long>> BalanceAsync(Guid from, Guid to) {
      return await _balanceService.BalanceAsync(from, to);
    }

    private async Task<bool> ParticipantExistsAsync(Guid id) {
      return (await _participantService.GetAsync(id)) != null;
    }
  }
}