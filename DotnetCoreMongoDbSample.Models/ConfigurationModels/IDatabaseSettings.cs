using System;
using System.Collections.Generic;
using System.Text;

namespace DotnetCoreMongoDbSample.Models.ConfigurationModels
{
    public interface IDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
