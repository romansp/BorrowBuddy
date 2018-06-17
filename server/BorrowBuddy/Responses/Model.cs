namespace BorrowBuddy.Responses {
  public static class Model {
    public static Flow Map(Domain.Flow f) {
      return new Flow() {
        Id = f.Id,
        Amount = f.Amount,
        Comment = f.Comment,
        Lendee = Map(f.Lendee),
        Lender = Map(f.Lender),
        Timestamp = f.Timestamp
      };
    }

    public static Participant Map(Domain.Participant p) {
      return new Participant() {
        Id = p.Id,
        FirstName = p.FirstName,
        LastName = p.LastName,
        MiddleName = p.MiddleName
      };
    }

    public static Currency Map(Domain.Currency c) {
      return new Currency() {
        Code = c.Code,
        Scale = c.Scale,
        Symbol = c.Symbol
      };
    }
  }
}
