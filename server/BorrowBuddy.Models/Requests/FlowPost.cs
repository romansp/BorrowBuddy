using System;

namespace BorrowBuddy.Models.Requests {
  public class FlowPost {
    public Guid Lender { get; set; }
    public Guid Lendee { get; set; }
    public long Amount { get; set; }
    public string CurrencyCode { get; set; }
    public string Comment { get; set; }
  }
}
