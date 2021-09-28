using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace Template.API.Application.Tests
{
    public class WebServerShould
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public WebServerShould()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task ReturnPong()
        {
            var response = await _client.GetAsync("/API");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal("Pong", responseString);
        }
        
        [Fact]
        public void RouteShouldNotHaveVerbs()
        {
            var service = _server.Host.Services.GetService(typeof(IActionDescriptorCollectionProvider)) as IActionDescriptorCollectionProvider;
            Assert.NotNull(service);
            
            string[] verbs = { "create", "get", "post", "put", "delete", "fetch", "generate" };
            
            Regex rx = new Regex($"({string.Join("|", verbs)})", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            
            foreach (var i in service.ActionDescriptors.Items)
            {
                var route = i.AttributeRouteInfo.Template;
                Assert.NotNull(route);
                MatchCollection matches = rx.Matches(route);
                Assert.Empty(matches);
            }
        }
    }

}

