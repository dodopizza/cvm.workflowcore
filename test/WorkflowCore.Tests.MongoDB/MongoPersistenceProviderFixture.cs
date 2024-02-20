using System;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using WorkflowCore.Interface;
using WorkflowCore.Persistence.MongoDB.Services;
using WorkflowCore.UnitTests;
using Xunit;

namespace WorkflowCore.Tests.MongoDB
{
    [Collection("Mongo collection")]
    public class MongoPersistenceProviderFixture : BasePersistenceFixture
    {
        private readonly MongoDockerSetup _dockerSetup;

        static MongoPersistenceProviderFixture()
        {
            BsonSerializer.RegisterSerializer(
                new ObjectSerializer(
                    type => type.FullName.StartsWith("WorkflowCore.UnitTests")
                            || type.FullName.StartsWith("WorkflowCore.IntegrationTests")));
        }

        public MongoPersistenceProviderFixture(MongoDockerSetup dockerSetup)
        {
            _dockerSetup = dockerSetup;
        }

        protected override IPersistenceProvider Subject
        {
            get
            {
                var db = _dockerSetup.Client.GetDatabase(nameof(MongoPersistenceProviderFixture));
                return new MongoPersistenceProvider(db);
            }
        }
    }
}