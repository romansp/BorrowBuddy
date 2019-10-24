using BorrowBuddy.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace BorrowBuddy.Data {
  public class BorrowBuddyContext : DbContext {
    public BorrowBuddyContext(DbContextOptions<BorrowBuddyContext> options)
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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
      if (!optionsBuilder.IsConfigured) {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFProviders.InMemory;Trusted_Connection=True;ConnectRetryCount=0");
      }
    }
  }
}
