using System;
using BorrowBuddy.Domain;

namespace BorrowBuddy.Responses {

  public class Flow {
    public Guid Id { get; set; }
    public Participant Lender { get; set; }
    public Participant Lendee { get; set; }
    public Money Amount { get; set; }
    public string Comment { get; set; }
    public DateTimeOffset Timestamp { get; set; }


  }
}
