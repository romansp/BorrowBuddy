using System;

namespace BorrowBuddy.Domain {
  public class Flow {
    public long Id { get; set; }
    public DateTimeOffset Timestamp { get; set; }
    public Participant Lender { get; set; }
    public Participant Lendee { get; set; }
    public Money Amount { get; set; }
    public string Comment { get; set; }
  }
}