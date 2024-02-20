using System;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Squadron;
using Xunit;

namespace WorkflowCore.Tests.MongoDB
{
    public class MongoDockerSetup : IAsyncLifetime
    {
        private readonly MongoReplicaSetResource _mongoResource;
        public static string ConnectionString { get; private set; }
        public IMongoClient Client => _mongoResource.Client;

        static MongoDockerSetup()
        {
            BsonSerializer.RegisterSerializer(
                new ObjectSerializer(
                    type => ObjectSerializer.DefaultAllowedTypes(type)
                            || type.FullName.StartsWith("WorkflowCore"))
                );
        }

        public MongoDockerSetup()
        {
            _mongoResource = new MongoReplicaSetResource();
        }

        public async Task InitializeAsync()
        {
            await _mongoResource.InitializeAsync();
            ConnectionString = _mongoResource.ConnectionString;
        }

        public Task DisposeAsync()
        {
            return _mongoResource.DisposeAsync();
        }
    }

    [CollectionDefinition("Mongo collection")]
    public class MongoCollection : ICollectionFixture<MongoDockerSetup>
    {
    }
}