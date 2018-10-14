using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin.Testing;
using Owin;
using Xunit;

namespace Example.WebApi.IntegrationTests
{
    public class ValuesTests
    {
        [Fact]
        public async Task Should_ReturnValues_When_GetValuesIsCalled()
        {
            // [arrange]
            using (var testServer = CreateTestServer())
            {
                var httpClient = new HttpClient(testServer.Handler);

                var expectedValues = new[] { "value1", "value2" };

                // [act]
                var response = await httpClient.GetAsync(testServer.BaseAddress + "/" + "api/values/");

                // [assert]
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                var listResponse = await response.Content.ReadAsAsync<IReadOnlyList<string>>();

                Assert.Equal(expectedValues, listResponse);
            }
        }

        private TestServer CreateTestServer()
        {
            return TestServer.Create(CreateConfiguration);
        }

        private void CreateConfiguration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();

            WebApiConfig.Register(config);

            appBuilder.UseWebApi(config);
        }
    }
}
