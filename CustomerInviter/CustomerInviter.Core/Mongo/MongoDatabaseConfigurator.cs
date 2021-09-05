using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;
using Serilog;

namespace CustomerInviter.Core.Mongo
{
    public static class MongoDatabaseConfigurator
    {
        static MongoDatabaseConfigurator()
        {
            RegisterConventions();
            RegisterClassMaps();
        }

        public static IMongoDatabase Configure(string connectionString, bool debugLogging = true)
        {
            var mongoConnectionString = MongoUrl.Create(connectionString);
            var settings = MongoClientSettings.FromUrl(mongoConnectionString);
            if (debugLogging)
            {
                settings.ClusterConfigurator = cb =>
                {
                    cb.Subscribe<CommandStartedEvent>(e =>
                    {
                        if (e.OperationId == null)
                            return;
                        Log.Debug("MongoDB command {commandName}: {command}", e.CommandName, e.Command);
                    });
                };
            }

            var client = new MongoClient(settings);
            return client.GetDatabase(mongoConnectionString.DatabaseName);
        }

        private static void RegisterConventions()
        {
            BsonSerializer.RegisterIdGenerator(typeof(string), StringObjectIdGenerator.Instance);
            ConventionRegistry.Register("camel case", new ConventionPack {new CamelCaseElementNameConvention()},
                t => true);
        }

        private static void RegisterClassMaps()
        {

        }
    }
}