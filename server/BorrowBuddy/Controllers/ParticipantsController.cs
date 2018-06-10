using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BorrowBuddy.Data;
using BorrowBuddy.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BorrowBuddy.Controllers {

  [ApiController]
  [Route("api/[controller]")]
  public class ParticipantsController : ControllerBase {
    private readonly BorrowBuddyContext _context;

    public ParticipantsController(BorrowBuddyContext context) {
      _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Participant>> GetParticipant() {
      return _context.Participants;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Participant>> GetParticipant(Guid id) {
      var participant = await _context.Participants.FirstOrDefaultAsync(m => m.Id == id);

      if (participant == null) {
        return NotFound();
      }

      return participant;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutParticipant([FromRoute] Guid id, [FromBody] Participant participant) {
      if (id != participant.Id) {
        return BadRequest();
      }

      _context.Entry(participant).State = EntityState.Modified;

      try {
        await _context.SaveChangesAsync();
      } catch (DbUpdateConcurrencyException) {
        if (!await ParticipantExistsAsync(id)) {
          return NotFound();
        }

        throw;
      }

      return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<ActionResult<Participant>> PostParticipant(Participant participant) {
      _context.Participants.Add(participant);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetParticipant), new { id = participant.Id }, participant);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteParticipant(Guid id) {
      var participant = await _context.Participants.FirstOrDefaultAsync(m => m.Id == id);
      if (participant == null) {
        return NotFound();
      }

      _context.Participants.Remove(participant);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private Task<bool> ParticipantExistsAsync(Guid id) {
      return _context.Participants.AnyAsync(e => e.Id == id);
    }
  }
}