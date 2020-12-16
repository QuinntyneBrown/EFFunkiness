using EFFunkiness.Server;
using EFFunkiness.Server.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace EFFunkiness.FunctionalTests
{
    public class ApiTestFixture : WebApplicationFactory<Startup>
    {
        public EFFunkinessDbContext Context { get; private set; }
        //protected override void ConfigureWebHost(IWebHostBuilder builder)
        //{            
        //    builder.ConfigureServices(services =>
        //    {
        //        var serviceProvider = services.BuildServiceProvider();

        //        using (var scope = serviceProvider.CreateScope())
        //        {
        //            var scopedServices = scope.ServiceProvider;
                    
        //            Context = scopedServices.GetRequiredService<EFFunkinessDbContext>();

        //            Context.Database.EnsureCreated();

        //            SeedData.Seed(Context, ConfigurationFactory.Create());
        //        }
        //    });
        //}

        public HttpClient CreateClient(bool enableRetryOnFailure = false)
        {
;

            var client = WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var serviceProvider = services.BuildServiceProvider();

                    using (var scope = serviceProvider.CreateScope())
                    {
                        var scopedServices = scope.ServiceProvider;

                        Context = scopedServices.GetRequiredService<EFFunkinessDbContext>();

                        var configuration = scopedServices.GetRequiredService<IConfiguration>();

                        configuration["EnableRetryOnFailure"] = $"{enableRetryOnFailure}";

                        Context.Database.EnsureCreated();

                        SeedData.Seed(Context, ConfigurationFactory.Create());
                    }

                });
            }).CreateClient();

            return client;
        }
    }
}
