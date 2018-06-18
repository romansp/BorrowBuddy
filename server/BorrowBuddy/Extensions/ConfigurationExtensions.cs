using BorrowBuddy.Configuration;
using Microsoft.Extensions.Configuration;

namespace BorrowBuddy.Extensions {
  public static class ConfigurationExtensions {
    public static string GetConnectionString(this IConfiguration configuration) {
      var appConfig = new AppConfig();
      var appConfigSection = configuration.GetSection(AppConfig.ConfigSection);
      appConfigSection.Bind(appConfig);
      if(appConfig.DbDialect == DbDialect.MySql) {
        var environmentVariableConnectionString = configuration["MYSQLCONNSTR_localdb"];
        if(!string.IsNullOrEmpty(environmentVariableConnectionString)) {
          return environmentVariableConnectionString;
        }
      }
      return configuration.GetConnectionString("BorrowBuddyContext");
    }
  }
}
