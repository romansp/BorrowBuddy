namespace BorrowBuddy.Models.Resources {
  public class FlowLog {
    public Flow Flow { get; set; }
    public Participant Lender { get; set; }
    public Participant Lendee { get; set; }
  }
}
