using EFFunkiness.Server;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;

namespace EFFunkiness.FunctionalTests
{
    public static class ConfigurationFactory
    {
        private static IConfiguration configuration;
        public static IConfiguration Create(bool enableRetryOnFailure = false)
        {
            if (configuration == null)
            {
                var basePath = Path.GetFullPath("../../../../../src/EFFunkiness.Server");

                configuration = new ConfigurationBuilder()
                    .SetBasePath(basePath)                    
                    .AddJsonFile("appsettings.json", false)
                    .AddUserSecrets<Startup>()
                    .AddInMemoryCollection(new Dictionary<string,string>() {
                        {"EnableRetryOnFailure", $"{enableRetryOnFailure}" }
                    })
                    .Build();
            }

            return configuration;
        }
    }
}
