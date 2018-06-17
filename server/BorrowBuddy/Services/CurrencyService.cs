using System;
using System.Collections.Generic;
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

    public Task<List<Currency>> GetAsync() {
      return _context.Currencies.ToListAsync();
    }

    public Task<Currency> GetAsync(string code) {
      if(code == null)
        throw new ArgumentNullException(nameof(code));

      return _context.Currencies.FirstOrDefaultAsync(c => c.Code == code);
    }

    public async Task<Currency> AddAsync(CurrencyDto dto) {
      var currency = new Currency {
        Code = dto.Code,
        Scale = dto.Scale,
        Symbol = dto.Symbol
      };

      _context.Currencies.Add(currency);

      await _context.SaveChangesAsync();
      return currency;
    }

    public async Task<Currency> UpdateAsync(string code, CurrencyDto dto) {
      var currency = await GetAsync(code);
      currency.Symbol = dto.Symbol;
      currency.Scale = dto.Scale;
      await _context.SaveChangesAsync();
      return currency;
    }

    public async Task DeleteAsync(string code) {
      var currency = await GetAsync(code);
      _context.Currencies.Remove(currency);
      await _context.SaveChangesAsync();
    }
  }
}
