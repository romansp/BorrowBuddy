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
  public class FlowsController : ControllerBase {
    private readonly FlowService _flowService;

    public FlowsController(FlowService flowService) {
      _flowService = flowService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Flow>>> GetFlows() {
      return (await _flowService.GetAsync()).Select(Mapper.Map).ToList();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Flow>> GetFlow(Guid id) {
      var flow = await _flowService.GetAsync(id);

      if (flow == null) {
        return NotFound();
      }

      return Mapper.Map(flow);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutFlow([FromRoute] Guid id, [FromBody] Flow model) {
      if(model.Id != id) {
        return BadRequest();
      }

      if(!await FlowExistsAsync(id)) {
        return NotFound();
      }

      var flow = _flowService.UpdateAsync(id,
        new FlowDto {
          Amount = model.Amount,
          CurrencyCode = model.CurrencyCode,
          LenderId = model.Lender,
          LendeeId = model.Lendee,
          Comment = model.Comment
        });

      return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<ActionResult<Flow>> PostFlow(FlowPost model) {
      var flow = await _flowService.AddAsync(new FlowDto {
        Amount = model.Amount,
        CurrencyCode = model.CurrencyCode,
        LenderId = model.Lender,
        LendeeId = model.Lendee,
        Comment = model.Comment
      });

      return CreatedAtAction(nameof(GetFlow), new { id = flow.Id }, Mapper.Map(flow));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFlow(Guid id) {
      if(!await FlowExistsAsync(id)) {
        return NotFound();
      }

      await _flowService.DeleteAsync(id);
      return NoContent();
    }

    private async Task<bool> FlowExistsAsync(Guid id) {
      return (await _flowService.GetAsync(id)) != null;
    }
  }
}