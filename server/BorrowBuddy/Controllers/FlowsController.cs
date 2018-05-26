using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BorrowBuddy.Data;
using BorrowBuddy.Domain;

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
    public async Task<ActionResult<Flow>> GetFlow(long id) {
      var flow = await _context.Flows.FindAsync(id);

      if (flow == null) {
        return NotFound();
      }

      return Ok(flow);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutFlow([FromRoute] long id, [FromBody] Flow flow) {
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
    public async Task<ActionResult<Flow>> PostFlow(Flow flow) {
      _context.Flows.Add(flow);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetFlow), new { id = flow.Id }, flow);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFlow(long id) {
      var flow = await _context.Flows.FindAsync(id);
      if (flow == null) {
        return NotFound();
      }

      _context.Flows.Remove(flow);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private Task<bool> FlowExistsAsync(long id) {
      return _context.Flows.AnyAsync(e => e.Id == id);
    }
  }

}