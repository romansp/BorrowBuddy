using System.Threading.Tasks;
using BorrowBuddy.Data;
using Microsoft.EntityFrameworkCore;

namespace BorrowBuddy {
  public class DbMigrator {
    private readonly BorrowBuddyContext _context;

    public DbMigrator(BorrowBuddyContext context) {
      _context = context;
    }

    public Task Migrate() {
      return _context.Database.MigrateAsync();
    }
  }
}