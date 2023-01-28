using Autofac;
using Autofac.Extensions.DependencyInjection.AzureFunctions;
using Kbvm.TriMetTransitInfo.Functions;
using Kbvm.TriMetTransitInfo.Functions.Interfaces;
using Kbvm.TriMetTransitInfo.Functions.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

[assembly: FunctionsStartup(typeof(Startup))]

namespace Kbvm.TriMetTransitInfo.Functions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient<IGetLatLong, PositionStackForwardGeoCode>((svcs, cfg) => RegisterUrl(svcs, cfg, "PositionStackUrl"));
            builder.Services.AddHttpClient<IGetStopInfo, TriMetStopInfo>((svcs, cfg) => RegisterUrl(svcs, cfg, "TriMetUrl"));
            builder.Services.AddHttpClient<IGetArrivalInfo, TriMetArrivalInfo>((svcs, cfg) => RegisterUrl(svcs, cfg, "TriMetUrl"));

            builder.UseAutofacServiceProviderFactory(ConfigureContainer);
        }

		public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            builder.UseAppSettings();
        }

        private IContainer ConfigureContainer(ContainerBuilder builder)
        {
            // The function class itself will be created using autofac
            builder
                .RegisterAssemblyTypes(typeof(Startup).Assembly)
                .InNamespace("Kbvm.TriMetTransitInfo.Functions.Endpoints")
                .AsSelf()
                .InstancePerTriggerRequest();

            // Configuration
            builder.Register(cfg => new PositionStackConfig(cfg.Resolve<IConfiguration>()["PositionStackApiKey"]))
                .AsSelf()
                .SingleInstance();

            builder.Register(cfg =>
            {
                var config = cfg.Resolve<IConfiguration>();
                return new TriMetConfig(
                    config["TriMetApiId"],
                    int.Parse(config["TriMetDistanceToSearch"]),
                    int.Parse(config["TriMetArrivalWindowMinutes"]));
            })
                .AsSelf()
                .SingleInstance();

            builder.RegisterType<ArrivalHandler>().As<IArrivalHandler>().InstancePerTriggerRequest();

            return builder.Build();
        }

		private static void RegisterUrl(IServiceProvider svcs, HttpClient cfg, string configKey)
		{
			IConfiguration config = svcs.GetRequiredService<IConfiguration>();
			cfg.BaseAddress = new Uri(config[configKey]);
		}
	}

    public record PositionStackConfig (string AccessKey);
    public record TriMetConfig(string ApplicationId, int DistanceToSearch, int ArrivalWindowInMinutes);
}
