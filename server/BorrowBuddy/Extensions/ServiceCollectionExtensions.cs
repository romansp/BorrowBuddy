using BorrowBuddy.Configuration;
using BorrowBuddy.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BorrowBuddy.Extensions {
  public static class ServiceCollectionExtensions {
    public static IServiceCollection AddDbContext(this IServiceCollection services, DbDialect dbDialect, string connectionString) {
      services.AddDbContextPool<BorrowBuddyContext>(options => {
        options.UseLazyLoadingProxies();
        switch(dbDialect) {
          case DbDialect.MySql: {
              options.UseMySql(connectionString);
              break;
            }
          case DbDialect.SqlServer: {
              options.UseSqlServer(connectionString);
              break;
            }
        }
      });
      return services;
    }
  }
}
