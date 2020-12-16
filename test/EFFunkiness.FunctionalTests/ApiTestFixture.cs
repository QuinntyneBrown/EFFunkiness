using EFFunkiness.Server;
using EFFunkiness.Server.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace EFFunkiness.FunctionalTests
{
    public class ApiTestFixture : WebApplicationFactory<Startup>
    {
        public EFFunkinessDbContext Context { get; private set; }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var serviceProvider = services.BuildServiceProvider();

                using (var scope = serviceProvider.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    
                    Context = scopedServices.GetRequiredService<EFFunkinessDbContext>();

                    Context.Database.EnsureCreated();

                    SeedData.Seed(Context, ConfigurationFactory.Create());
                }
            });
        }
    }
}
