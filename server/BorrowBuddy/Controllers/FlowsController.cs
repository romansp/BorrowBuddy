using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BorrowBuddy.Data;
using BorrowBuddy.Domain;
using System;

namespace BorrowBuddy.Controllers {

  [ApiController]
  [Route("api/[controller]")]
  public class FlowsController : ControllerBase {
    private readonly BorrowBuddyContext _context;

    public FlowsController(BorrowBuddyContext context) {
      _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Flow>> GetFlows() {
      return _context.Flows;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Flow>> GetFlow(Guid id) {
      var flow = await _context.Flows.FirstOrDefaultAsync(m => m.Id == id);

      if (flow == null) {
        return NotFound();
      }

      return Ok(flow);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutFlow([FromRoute] Guid id, [FromBody] Flow flow) {
      if (id != flow.Id) {
        return BadRequest();
      }

      _context.Entry(flow).State = EntityState.Modified;

      try {
        await _context.SaveChangesAsync();
      } catch (DbUpdateConcurrencyException) {
        if (!(await FlowExistsAsync(id))) {
          return NotFound();
        } else {
          throw;
        }
      }

      return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<ActionResult<Flow>> PostFlow(Models.Requests.FlowPost flowPost) {
      var flow = new Flow() {
        Amount = new Money() {
          Value = flowPost.Amount,
          Currency = await _context.Currencies.FirstAsync(c => c.Code == "BYN")
        },
        Comment = flowPost.Comment,
        Timestamp = DateTimeOffset.UtcNow,
        Lendee = await _context.Participants.FirstOrDefaultAsync(c => c.Id == flowPost.LendeeId),
        Lender = await _context.Participants.FirstOrDefaultAsync(c => c.Id == flowPost.LenderId)
      };
      _context.Flows.Add(flow);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetFlow), new { id = flow.Id }, flow);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFlow(Guid id) {
      var flow = await _context.Flows.FirstOrDefaultAsync(m => m.Id == id);
      if (flow == null) {
        return NotFound();
      }

      _context.Flows.Remove(flow);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private Task<bool> FlowExistsAsync(Guid id) {
      return _context.Flows.AnyAsync(e => e.Id == id);
    }
  }

}