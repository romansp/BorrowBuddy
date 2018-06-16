using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BorrowBuddy.Data;
using BorrowBuddy.Domain;
using BorrowBuddy.Dto;
using Microsoft.EntityFrameworkCore;

namespace BorrowBuddy.Services {

  public class FlowService {
    private readonly BorrowBuddyContext _context;

    public FlowService(BorrowBuddyContext context) {
      _context = context;
    }

    public Task<List<Flow>> GetAsync() {
      return _context.Flows.ToListAsync();
    }

    public Task<Flow> GetAsync(Guid id) {
      return _context.Flows.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Flow> AddAsync(FlowDto dto) {
      var lendee = await _context.Participants.FirstOrDefaultAsync(c => c.Id == dto.LendeeId);
      var lender = await _context.Participants.FirstOrDefaultAsync(c => c.Id == dto.LenderId);
      var currency = await _context.Currencies.FirstOrDefaultAsync(c => c.Code == dto.Code);

      var flow = new Flow {
        Lendee = lendee,
        Lender = lender,
        Comment = dto.Comment,
        Timestamp = DateTimeOffset.UtcNow,
        Amount = new Money() {
          Currency = currency,
          Value = dto.Amount
        }
      };
      _context.Flows.Add(flow);
      await _context.SaveChangesAsync();
      return flow;
    }

    public async Task<Flow> UpdateAsync(Guid id, FlowDto dto) {
      var flow = await GetAsync(id);
      var lendee = await _context.Participants.FirstOrDefaultAsync(c => c.Id == dto.LendeeId);
      var lender = await _context.Participants.FirstOrDefaultAsync(c => c.Id == dto.LenderId);
      var currency = await _context.Currencies.FirstOrDefaultAsync(c => c.Code == dto.Code);

      flow.Lendee = lendee;
      flow.Lender = lender;
      flow.Comment = dto.Comment;
      flow.Amount = new Money() {
        Currency = currency,
        Value = dto.Amount
      };
      await _context.SaveChangesAsync();

      return flow;
    }

    public async Task DeleteAsync(Guid id) {
      var flow = await GetAsync(id);
      _context.Flows.Remove(flow);
      await _context.SaveChangesAsync();
    }
  }
}
