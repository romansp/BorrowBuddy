using System;
using System.Linq;
using System.Threading.Tasks;
using BorrowBuddy.Data;
using BorrowBuddy.Domain;
using Microsoft.EntityFrameworkCore;

namespace BorrowBuddy.Services {
  public class BalanceService {
    private readonly BorrowBuddyContext _context;

    public BalanceService(BorrowBuddyContext context) {
      _context = context;
    }

    public async Task<long> BalanceAsync(Guid lenderId, Guid lendeeId, string code) {
      var from = await _context.Participants.FirstOrDefaultAsync(c => c.Id == lenderId);
      var to = await _context.Participants.FirstOrDefaultAsync(c => c.Id == lendeeId);
      return from.Balance(to, code) - to.Balance(from, code);
    }
  }

  public static class BalanceExtensions {
    public static long Balance(this Participant from, Participant to, string code) {
      var outcomingFlows = from.Lended.Where(c => c.Lendee == to && c.Amount.Currency.Code == code);
      return outcomingFlows.Sum(c => c.Amount.Value);
    }
  }
}
