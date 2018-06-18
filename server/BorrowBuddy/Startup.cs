using BorrowBuddy.Data;
using BorrowBuddy.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

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
      services.AddDbContextPool<BorrowBuddyContext>(options => options
              .UseLazyLoadingProxies()
              .UseSqlServer(Configuration.GetConnectionString("BorrowBuddyContext")));

      services.AddSwaggerGen(c => c.SwaggerDoc("api", new Info { Title = "BorrowBuddy" }));
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
      app.UseDefaultFiles();
      app.UseStaticFiles();
      if(env.IsDevelopment()) {
        app.UseDeveloperExceptionPage();
        app.UseCors("CorsPolicy");
      } else {
        app.UseHsts();
      }
      app.UseHttpsRedirection();
      app.UseMvc();
      app.UseSwagger();
      app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/api/swagger.json", "BorrowBuddy"));
    }
  }
}
