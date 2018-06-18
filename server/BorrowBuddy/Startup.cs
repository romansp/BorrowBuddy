using BorrowBuddy.Services;
using BorrowBuddy.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using BorrowBuddy.Configuration;

namespace BorrowBuddy {
  public class Startup {
    public Startup(IConfiguration configuration) {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services) {
      services.AddTransient<CurrencyService>();
      services.AddTransient<ParticipantService>();
      services.AddTransient<FlowService>();
      services.AddTransient<BalanceService>();
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
      services.AddCors(options => {
        options.AddPolicy("CorsPolicy",
      builder => builder.AllowAnyOrigin()
      .AllowAnyMethod()
      .AllowAnyHeader()
      .AllowCredentials());
      });

      services.AddScoped<DbMigrator>();

      var appConfig = new AppConfig();
      var appConfigSection = Configuration.GetSection(AppConfig.ConfigSection);
      appConfigSection.Bind(appConfig);
      services.Configure<AppConfig>(Configuration.GetSection(AppConfig.ConfigSection));

      var connectionString = Configuration.GetConnectionString();
      services.AddDbContext(appConfig.DbDialect, connectionString);
      services.AddSwaggerGen(c => c.SwaggerDoc("api", new Info { Title = "BorrowBuddy" }));
    }


    public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
      if(env.IsDevelopment()) {
        app.UseDeveloperExceptionPage();
        app.UseCors("CorsPolicy");
      } else {
        app.UseHsts();
      }
      app.UseHttpsRedirection();
      app.UseDefaultFiles();
      app.UseStaticFiles();
      app.UseMvc();
      app.UseSwagger();
      app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/api/swagger.json", "BorrowBuddy"));
    }
  }

}
