using Microsoft.EntityFrameworkCore;
using BorrowBuddy.Domain;

namespace BorrowBuddy.Data {
  public class BorrowBuddyContext : DbContext {

    public BorrowBuddyContext(DbContextOptions options)
        : base(options) {
    }

    public DbSet<Flow> Flows { get; set; }
    public DbSet<Participant> Participants { get; set; }
    public DbSet<Currency> Currencies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
      modelBuilder.ApplyConfiguration(new Configuration.Flow());
      modelBuilder.ApplyConfiguration(new Configuration.Participant());
      modelBuilder.ApplyConfiguration(new Configuration.Currency());

      base.OnModelCreating(modelBuilder);
    }
  }
}
