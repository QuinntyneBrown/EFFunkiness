using EFFunkiness.Server;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace EFFunkiness.FunctionalTests
{
    public static class ConfigurationFactory
    {
        private static IConfiguration configuration;
        public static IConfiguration Create()
        {
            if (configuration == null)
            {
                var basePath = Path.GetFullPath("../../../../../src/EFFunkiness.Server");

                configuration = new ConfigurationBuilder()
                    .SetBasePath(basePath)
                    .AddJsonFile("appsettings.json", false)
                    .AddUserSecrets<Startup>()
                    .Build();
            }

            return configuration;
        }
    }
}
