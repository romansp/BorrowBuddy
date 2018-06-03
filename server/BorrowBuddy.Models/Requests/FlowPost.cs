using System;

namespace BorrowBuddy.Models.Requests {
  public class FlowPost {
    public long Amount { get; set; }
    public string Comment { get; set; }
    public Guid To { get; set; }
    public Guid From { get; set; }
  }
}
