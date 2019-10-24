using BorrowBuddy.Services;
using BorrowBuddy.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.OpenApi.Models;

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
      services.AddResponseCaching();
      services.AddApplicationInsightsTelemetry();
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
      services.AddCors(options => {
          options.AddPolicy("CorsPolicy", builder => builder 
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .SetIsOriginAllowed(origin => true)
                    );
        }
      );

      var appConfig = services.AddAppConfig(Configuration);
      var connectionString = Configuration.GetConnectionString();
      services.AddDbContext(appConfig.DbDialect, connectionString);
      services.AddScoped<DbMigrator>();
      services.AddSwaggerGen(c => c.SwaggerDoc("api", new OpenApiInfo { Title = "BorrowBuddy" }));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
      if (env.IsDevelopment()) {
        app.UseDeveloperExceptionPage();
      } else {
        app.UseHsts();
      }
      app.UseHttpsRedirection();
      app.UseResponseCaching();
      app.UseDefaultFiles();
      app.UseStaticFiles(new StaticFileOptions() {
        OnPrepareResponse = ctx => {
          if (!string.Equals("service-worker.js", ctx.File.Name, StringComparison.OrdinalIgnoreCase)) {
            return;
          }
          var headers = ctx.Context.Response.GetTypedHeaders();
          headers.CacheControl = new CacheControlHeaderValue {
            MaxAge = TimeSpan.FromMinutes(1),
            NoCache = true,
            NoStore = true
          };
          headers.Expires = DateTimeOffset.UtcNow.AddDays(-1);
        }
      });

      app.UseRouting();
      app.UseCors();

      if (env.IsDevelopment()) {
        app.UseCors("CorsPolicy");
      }

      app.UseEndpoints(endpoints => {
        endpoints.MapDefaultControllerRoute();
      });
      app.UseSwagger();
      app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/api/swagger.json", "BorrowBuddy"));
    }
  }

}
