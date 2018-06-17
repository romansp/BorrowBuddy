using BorrowBuddy.Models.Resources;

namespace BorrowBuddy.Models {
  public static class Mapper {
    public static Flow Map(Domain.Flow f) {
      return new Flow {
        Id = f.Id,
        Amount = f.Amount.Value,
        CurrencyCode = f.Amount.Currency.Code,
        CurrencyScale = f.Amount.Currency.Scale,
        Comment = f.Comment,
        Lendee = f.Lendee.Id,
        Lender = f.Lender.Id,
        Timestamp = f.Timestamp
      };
    }

    public static Participant Map(Domain.Participant p) {
      return new Participant {
        Id = p.Id,
        FirstName = p.FirstName,
        LastName = p.LastName,
        MiddleName = p.MiddleName
      };
    }

    public static Currency Map(Domain.Currency c) {
      return new Currency {
        Code = c.Code,
        Scale = c.Scale,
        Symbol = c.Symbol
      };
    }
  }
}
