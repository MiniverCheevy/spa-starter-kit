using System;
using System.Diagnostics;
using System.IO;
using Fernweh.Core;
using Fernweh.Infrastructure;
using Fernweh.Infrastructure.Authentication;
using Fernweh.Infrastructure.ExceptionHandling;
using Fernweh.Infrastructure.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Voodoo;

namespace Fernweh
{
  public class Startup
  {
        public IConfigurationRoot Configuration { get; set; }
    public Startup(IHostingEnvironment env)
    {
      var builder = new ConfigurationBuilder();
      builder.AddEnvironmentVariables();
      builder
          .SetBasePath(env.ContentRootPath)
          .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
          .AddEnvironmentVariables();
      Console.WriteLine($"Environment: {env.EnvironmentName}");
      this.Configuration = builder.Build();
      IOC.Settings = SettingsFactory.GetSettings(builder.Build());

    }

    public IConfigurationRoot Configuration { get; set; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddApplicationInsightsTelemetry(Configuration);
      services.AddWebApi();
      services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
      //Error Handling should always be first
      app.UseMiddleware<AppErrorHandlingMiddleware>();
      app.UseMiddleware<CompositionMiddleware>();
      app.UseMiddleware<CacheBusterMiddleware>();
      //Token reader goes before authentication
      app.UseMiddleware<TokenReaderMiddleware>();
      app.UseMiddleware<WindowsAuthenticationMiddleware>();
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
        {
          HotModuleReplacement = true , 
        });

      }

      app.UseDefaultFiles();
      app.UseStaticFiles();
      updateDatabaseToLatestVersion(env);

      app.UseMvcWithDefaultRoute();
    }

    //TODO: replace with the equivalent to UpdateDatabaseToLatestVersion when there is one for aspnet.core
    private void updateDatabaseToLatestVersion(IHostingEnvironment env)
    {
      var file = Path.GetFullPath($@"{env.WebRootPath}\..\DbUpdate.exe");
      Console.Write(file);
      if (!File.Exists(file))
        return;
      Console.Write(" Found");

      var connectionString = Objectifyer.Base64Encode(IOC.GetConnectionString());
      var psi = new ProcessStartInfo
      {
        FileName = file,
        Arguments = connectionString
      };

      var proc = Process.Start(psi);
      proc.WaitForExit();
    }
  }
}