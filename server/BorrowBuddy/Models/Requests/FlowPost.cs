using System;

namespace BorrowBuddy.Models.Requests {
  public class FlowPost {
    public long Amount { get; set; }
    public string Comment { get; set; }
    public Guid LendeeId { get; set; }
    public Guid LenderId { get; set; }
  }
}
