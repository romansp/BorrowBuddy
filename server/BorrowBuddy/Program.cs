using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace BorrowBuddy {
  public static class Program {
    public static void Main(string[] args) {
      var host = CreateWebHostBuilder(args).Build();
      using(var scope = host.Services.CreateScope()) {
        var services = scope.ServiceProvider;
        var migrator = services.GetRequiredService<DbMigrator>();
        migrator.Migrate();
      }

      host.Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseApplicationInsights()
            .UseStartup<Startup>();
  }
}
