using Autofac;
using CustomerInviter.Api.Service.AutofacModules;
using CustomerInviter.Core.AutofacModules;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Autofac;
using Nancy.Configuration;
using Nancy.Extensions;
using Newtonsoft.Json;
using Serilog;

namespace CustomerInviter.Api.Service
{
    public class Bootstrapper : AutofacNancyBootstrapper
    {
        public override void Configure(INancyEnvironment environment)
        {
            environment.Views(runtimeViewUpdates: true);
            base.Configure(environment);
        }

        protected override void RequestStartup(ILifetimeScope container, IPipelines pipelines, NancyContext context)
        {
            base.RequestStartup(container, pipelines, context);
            ConfigureErrorHandling(pipelines);
        }

        protected override ILifetimeScope GetApplicationContainer()
        {
            var builder = new ContainerBuilder();

            // Registers all modules in this assembly, if wanting a specific one just list the modules you wish to import
            builder.RegisterAssemblyModules(typeof(ServiceModule).GetAssembly(), typeof(ConfigurationModule).GetAssembly());
            builder.RegisterType<CustomJsonSerializer>().As<JsonSerializer>();

            var container = builder.Build();
            return container;
        }

        private static void ConfigureErrorHandling(IPipelines pipeline)
        {
            pipeline.OnError.AddItemToEndOfPipeline((context, ex) =>
            {
                Log.Error(ex, "An error occurred processing the request.");

                Response response = HttpStatusCode.InternalServerError;
                return response;
            });
        }
    }
}