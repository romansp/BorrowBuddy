using System;
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

    public async Task AddAsync(FlowDto dto) {
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
    }
  }
}
