using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Fernweh.Infrastructure
{
  //https://andrewlock.net/removing-the-mvc-razor-dependencies-from-the-web-api-template-in-asp-net-core/


  public static class WebApiServiceCollectionExtensions
  {
    /// <summary>
    ///   Adds MVC services to the specified <see cref="IServiceCollection" /> for Web API.
    ///   This is a slimmed down version of <see cref="MvcServiceCollectionExtensions.AddMvc" />
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
    /// <returns>An <see cref="IMvcBuilder" /> that can be used to further configure the MVC services.</returns>
    public static IMvcBuilder AddWebApi(this IServiceCollection services)
    {
      if (services == null) throw new ArgumentNullException(nameof(services));

      var builder = services.AddMvcCore();
      builder.AddAuthorization();

      builder.AddFormatterMappings();

      // +10 order
      builder.AddJsonFormatters()
        .AddJsonOptions(opt =>
        {
          var settings = opt.SerializerSettings;
          var resolver = settings.ContractResolver;

          //opt.SerializerSettings.Converters.Add(new UtcToLocalTimeConverter())
          settings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
          settings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
          settings.NullValueHandling = NullValueHandling.Ignore;
          settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
          //settings.DateParseHandling = DateParseHandling.
          if (resolver != null)
          {
            var res = resolver as DefaultContractResolver;

            //res.NamingStrategy = null;  // <<!-- this removes the camelcasing            
          }
        });

      builder.AddCors();

      return new MvcBuilder(builder.Services, builder.PartManager);
    }

    /// <summary>
    ///   Adds MVC services to the specified <see cref="IServiceCollection" /> for Web API.
    ///   This is a slimmed down version of <see cref="MvcServiceCollectionExtensions.AddMvc" />
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
    /// <param name="setupAction">An <see cref="Action{MvcOptions}" /> to configure the provided <see cref="MvcOptions" />.</param>
    /// <returns>An <see cref="IMvcBuilder" /> that can be used to further configure the MVC services.</returns>
    public static IMvcBuilder AddWebApi(this IServiceCollection services, Action<MvcOptions> setupAction)
    {
      if (services == null) throw new ArgumentNullException(nameof(services));
      if (setupAction == null) throw new ArgumentNullException(nameof(setupAction));

      var builder = services.AddWebApi();
      builder.Services.Configure(setupAction);

      return builder;
    }
  }
}
