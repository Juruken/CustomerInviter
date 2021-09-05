using System;
using Autofac;
using CustomerInviter.Core.Mongo;
using MongoDB.Driver;
using Nancy.Testing;
using Serilog;
using Xunit.Abstractions;

namespace CustomerInvite.Api.Service.Tests.Scenarios
{
    public abstract class ApiScenario
    {
        private string _connectionString;
        protected Browser Browser;
        protected IMongoDatabase Database;
        protected ITestOutputHelper Output;
        protected ILifetimeScope Scope;

        protected ApiScenario(ITestOutputHelper output)
        {
            Output = output;
        }

        public virtual void Setup()
        {
            ConfigureLogging(Output);

            _connectionString = $"mongodb://localhost/tonic-test-{Guid.NewGuid()}";

            DropDatabase();

            Database = MongoDatabaseConfigurator.Configure(_connectionString);

            var bootstrapper = new TestableBootstrapper();
            Scope = bootstrapper.Scope;
            Browser = new Browser(bootstrapper, context => context.Header("Accept", "application/json"));
        }

        /// <summary>
        /// Cleanup any testing resources we've created e.g. test databases etc.
        /// </summary>
        public virtual void TearDown()
        {
            DropDatabase();
        }

        private void DropDatabase()
        {
            var mongoConnectionString = MongoUrl.Create(_connectionString);
            var settings = MongoClientSettings.FromUrl(mongoConnectionString);

            var client = new MongoClient(settings);
            client.DropDatabase(mongoConnectionString.DatabaseName);
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
