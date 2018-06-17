using System;
using System.Collections.Generic;

namespace BorrowBuddy.Domain {
  public class Participant {
    public Participant() {
      Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public virtual ICollection<Flow> Lended { get; set; }
    public virtual ICollection<Flow> Borrowed { get; set; }
  }
}