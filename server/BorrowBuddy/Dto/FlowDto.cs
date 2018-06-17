using System;

namespace BorrowBuddy.Dto {
  public class FlowDto {
    public Guid LenderId { get; set; }
    public Guid LendeeId { get; set; }
    public string CurrencyCode { get; set; }
    public long Amount { get; set; }
    public string Comment { get; set; }
  }
}
