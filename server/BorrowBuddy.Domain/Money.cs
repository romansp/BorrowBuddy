namespace BorrowBuddy.Domain {
  public class Money {
    public long Value { get; set; }
    public virtual Currency Currency { get; set; }
  }
}