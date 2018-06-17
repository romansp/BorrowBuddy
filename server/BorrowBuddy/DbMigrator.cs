using BorrowBuddy.Data;
using Microsoft.EntityFrameworkCore;

namespace BorrowBuddy {
  public class DbMigrator {
    private readonly BorrowBuddyContext _context;

    public DbMigrator(BorrowBuddyContext context) {
      _context = context;
    }

    public void Migrate() {
      _context.Database.Migrate();
    }
  }
}