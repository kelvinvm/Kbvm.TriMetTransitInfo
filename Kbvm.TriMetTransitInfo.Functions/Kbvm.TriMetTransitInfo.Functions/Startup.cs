using Autofac;
using Autofac.Extensions.DependencyInjection.AzureFunctions;
using Kbvm.TriMetTransitInfo.Functions;
using Kbvm.TriMetTransitInfo.Functions.Interfaces;
using Kbvm.TriMetTransitInfo.Functions.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(Startup))]

namespace Kbvm.TriMetTransitInfo.Functions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient<IGetLatLong, PositionStackForwardGeoCode>((svcs, cfg) =>
            {
                IConfiguration confg = svcs.GetRequiredService<IConfiguration>();
                cfg.BaseAddress = new Uri(confg["PositionStackUrl"]);
            });
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

            builder.Register(cfg => new PositionStackConfig(cfg.Resolve<IConfiguration>()["PositionStackApiKey"]))
                .AsSelf()
                .SingleInstance();

            return builder.Build();
        }
    }

    public record PositionStackConfig (string AccessKey);
}
