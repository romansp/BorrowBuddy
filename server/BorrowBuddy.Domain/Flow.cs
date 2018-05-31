using System;

namespace BorrowBuddy.Domain {
  public class Flow {
    public Flow() {
      Id = Guid.NewGuid();
    }
    public Guid Id { get; set; }
    public virtual Participant Lender { get; set; }
    public virtual Participant Lendee { get; set; }
    public virtual Money Amount { get; set; }
    public string Comment { get; set; }
    public DateTimeOffset Timestamp { get; set; }
  }
}