using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using BorrowBuddy.Data;
using Microsoft.EntityFrameworkCore;

namespace BorrowBuddy.Test.Services {
  public abstract class ServiceTestBase {
    public static DbContextOptions<BorrowBuddyContext> BuildContextOptions([CallerFilePath]string databaseName = "", [CallerMemberName]string databaseNameSuffix = "") {
      if(string.IsNullOrEmpty(databaseName)) {
        databaseName = Guid.NewGuid().ToString();
      }
      if(string.IsNullOrEmpty(databaseNameSuffix)) {
        databaseNameSuffix = Guid.NewGuid().ToString();
      }
      var dbName = $"{databaseName}_{databaseNameSuffix}";
      return new DbContextOptionsBuilder<BorrowBuddyContext>()
                      .UseInMemoryDatabase(databaseName: dbName)
                      .UseLazyLoadingProxies()
                      .Options;
    }
  }
}
