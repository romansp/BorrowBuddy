using System.Threading.Tasks;
using BorrowBuddy.Data;
using BorrowBuddy.Domain;
using BorrowBuddy.Dto;
using Microsoft.EntityFrameworkCore;

namespace BorrowBuddy.Services {
  public class CurrencyService {
    private readonly BorrowBuddyContext _context;

    public CurrencyService(BorrowBuddyContext context) {
      _context = context;
    }

    public Task<Currency> GetAsync(string code) {
      if(code == null)
        throw new System.ArgumentNullException(nameof(code));

      return _context.Currencies.FirstOrDefaultAsync(c => c.Code == code);
    }

    public Task AddAsync(CurrencyDto dto) {
      _context.Currencies.Add(new Currency() {
        Code = dto.Code,
        Scale = dto.Scale,
        Symbol = dto.Symbol
      });

      return _context.SaveChangesAsync();
    }
  }
}
