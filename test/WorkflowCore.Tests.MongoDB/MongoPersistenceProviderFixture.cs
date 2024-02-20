using System;
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