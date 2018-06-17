using System;

namespace BorrowBuddy.Models.Resources {
  public class Participant {
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
  }
}
