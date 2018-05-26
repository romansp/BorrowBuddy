using Microsoft.EntityFrameworkCore;
using BorrowBuddy.Domain;

namespace BorrowBuddy.Data {
  public class BorrowBuddyContext : DbContext {
    public BorrowBuddyContext(DbContextOptions options)
        : base(options) {
    }

    public DbSet<Flow> Flows { get; set; }
    public DbSet<Participant> Participants { get; set; }
  }
}
