using System.Runtime.CompilerServices;
using BorrowBuddy.Data;
using Microsoft.EntityFrameworkCore;

namespace BorrowBuddy.Test.Services {
  public abstract class ServiceTestBase {
    public static DbContextOptions<BorrowBuddyContext> BuildContextOptions([CallerFilePath]string databaseName = "", [CallerMemberName]string databaseNameSuffix = "") {
      var dbName = $"{databaseName}_{databaseNameSuffix}";
      return new DbContextOptionsBuilder<BorrowBuddyContext>()
                      .UseInMemoryDatabase(databaseName: dbName)
                      .UseLazyLoadingProxies()
                      .Options;
    }
  }
}
