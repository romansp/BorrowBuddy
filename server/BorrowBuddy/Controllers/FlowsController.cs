using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BorrowBuddy.Models.Requests;
using BorrowBuddy.Responses;
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
    public async Task<ActionResult<IEnumerable<Flow>>> GetFlowsAsync() {
      return (await _flowService.GetAsync()).Select(Model.Map).ToList();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Flow>> GetFlow(Guid id) {
      var flow = await _flowService.GetAsync(id);

      if (flow == null) {
        return NotFound();
      }

      return Ok(Model.Map(flow));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutFlow([FromRoute] Guid id, [FromBody] FlowPost flowPut) {
      if(!await FlowExistsAsync(id)) {
        return NotFound();
      }

      var flow = _flowService.UpdateAsync(id,
        new Dto.FlowDto() {
          Amount = flowPut.Amount,
          Code = "BYN",
          LenderId = flowPut.From,
          LendeeId = flowPut.To,
          Comment = flowPut.Comment,
        });

      return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<ActionResult<Flow>> PostFlow(FlowPost flowPost) {
      var flow = await _flowService.AddAsync(new Dto.FlowDto() {
        Amount = flowPost.Amount,
        Code = "BYN",
        LenderId = flowPost.From,
        LendeeId = flowPost.To,
        Comment = flowPost.Comment,
      });

      return CreatedAtAction(nameof(GetFlow), new { id = flow.Id }, Model.Map(flow));
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