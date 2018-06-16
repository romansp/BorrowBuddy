using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BorrowBuddy.Responses;
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
      return (await _participantService.GetAsync()).Select(Model.Map).ToList();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Participant>> GetParticipant(Guid id) {
      var participant = await _participantService.GetAsync(id);

      if(participant == null) {
        return NotFound();
      }

      return Model.Map(participant);
    }

    [HttpGet("{from}/balance/{to}")]
    public async Task<ActionResult<long>> BalanceAsync(Guid from, Guid to) {
      return await _balanceService.BalanceAsync(from, to);
    }
  }
}