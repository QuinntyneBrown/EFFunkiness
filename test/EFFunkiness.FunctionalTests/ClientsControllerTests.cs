using EFFunkiness.Server.Queries;
using Newtonsoft.Json;
using System.Linq;
using Xunit;
using static EFFunkiness.FunctionalTests.ClientsControllerTests.Endpoints;

namespace EFFunkiness.FunctionalTests
{
    public class ClientsControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public ClientsControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetClients()
        {

            var httpResponseMessage = await _fixture.CreateClient().GetAsync(Get.Clients);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetClients.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.True(response.Clients.Any());

        }

        internal static class Endpoints
        {
            public static class Get
            {
                public static string Clients = "api/clients";
            }
        }
    }
}
