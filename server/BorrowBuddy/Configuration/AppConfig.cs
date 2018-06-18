using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BorrowBuddy.Configuration {
  public class AppConfig {
    public const string ConfigSection = nameof(AppConfig);

    public DbDialect DbDialect { get; set; }
  }
}
