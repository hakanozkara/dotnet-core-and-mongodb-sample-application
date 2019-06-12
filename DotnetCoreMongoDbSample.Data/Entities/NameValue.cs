using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotnetCoreMongoDbSample.Data.Entities
{
    public class NameValue : BaseEntity
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }
}
