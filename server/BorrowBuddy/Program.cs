using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace BorrowBuddy {
  public static class Program {
    public static void Main(string[] args) {
      var host = CreateHostBuilder(args).Build();
      using (var scope = host.Services.CreateScope()) {
        var services = scope.ServiceProvider;
        var migrator = services.GetRequiredService<DbMigrator>();
        migrator.Migrate();
      }

      host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
  }
}
