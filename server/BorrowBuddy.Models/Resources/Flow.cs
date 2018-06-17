using System;

namespace BorrowBuddy.Models.Resources {
  public class Flow {
    public Guid Id { get; set; }
    public Guid Lender { get; set; }
    public Guid Lendee { get; set; }
    public long Amount { get; set; }
    public string CurrencyCode { get; set; }
    public int CurrencyScale { get; internal set; }
    public string Comment { get; set; }
    public DateTimeOffset Timestamp { get; set; }
  }
}
