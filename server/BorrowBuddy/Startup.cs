using BorrowBuddy.Services;
using BorrowBuddy.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using BorrowBuddy.Configuration;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using System;

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
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
      services.AddCors(options =>
        options.AddPolicy("CorsPolicy", builder =>
          builder.AllowAnyOrigin()
                 .AllowAnyMethod()
                 .AllowAnyHeader()
                 .AllowCredentials()
          )
      );

      var appConfig = services.AddAppConfig(Configuration);
      var connectionString = Configuration.GetConnectionString();
      services.AddDbContext(appConfig.DbDialect, connectionString);
      services.AddScoped<DbMigrator>();
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
      app.UseResponseCaching();
      app.UseDefaultFiles();
      app.UseStaticFiles(new StaticFileOptions() {
        OnPrepareResponse = ctx => {
          if(!string.Equals("service-worker.js", ctx.File.Name, StringComparison.OrdinalIgnoreCase)) {
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
      app.UseMvc();
      app.UseSwagger();
      app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/api/swagger.json", "BorrowBuddy"));
    }
  }

}
