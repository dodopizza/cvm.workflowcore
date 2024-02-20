using System;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using WorkflowCore.IntegrationTests.Scenarios;
using Xunit;

namespace WorkflowCore.Tests.MongoDB.Scenarios
{
    [Collection("Mongo collection")]
    public class MongoUserScenario : UserScenario
    {
        static MongoUserScenario()
        {
            BsonSerializer.RegisterSerializer(
                new ObjectSerializer(
                    type => ObjectSerializer.DefaultAllowedTypes(type)));
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddWorkflow(x => x.UseMongoDB(MongoDockerSetup.ConnectionString, nameof(MongoUserScenario)));
        }
    }
}