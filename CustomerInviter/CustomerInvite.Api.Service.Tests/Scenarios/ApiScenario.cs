using Autofac;
using Nancy.Testing;
using Serilog;
using Xunit.Abstractions;

namespace CustomerInvite.Api.Service.Tests.Scenarios
{
    public abstract class ApiScenario
    {
        protected Browser Browser;
        protected ITestOutputHelper Output;
        protected ILifetimeScope Scope;

        protected ApiScenario(ITestOutputHelper output)
        {
            Output = output;
        }

        public virtual void Setup()
        {
            ConfigureLogging(Output);

            var bootstrapper = new TestableBootstrapper();
            Scope = bootstrapper.Scope;
            Browser = new Browser(bootstrapper, context => context.Header("Accept", "application/json"));
        }

        /// <summary>
        /// Cleanup any testing resources we've created e.g. test databases etc.
        /// </summary>
        public virtual void TearDown()
        {
            
        }

        private static void ConfigureLogging(ITestOutputHelper output)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.XunitTestOutput(output)
                .CreateLogger();
        }
    }
}
