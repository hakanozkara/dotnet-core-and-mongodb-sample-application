﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DotnetCoreMongoDbSample.Models.ConfigurationModels
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
