using DotnetCoreMongoDbSample.Data.Entities;
using DotnetCoreMongoDbSample.Models.ConfigurationModels;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotnetCoreMongoDbSample.Data
{
    public class SampleCollectionContext
    {
        private readonly IMongoDatabase _database;
        public SampleCollectionContext(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            if (client != null)
            {
                _database = client.GetDatabase(databaseSettings.DatabaseName);
            }
        }

        public IMongoCollection<NameValue> NameValues => _database.GetCollection<NameValue>("NameValues");

    }
}
