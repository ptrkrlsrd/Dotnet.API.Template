using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Template.API.Application.Tests;

public class WebApplicationFactoryTests : IClassFixture<WebApplicationFactory<Startup>>
{
    private readonly WebApplicationFactory<Startup> _factory;

    public WebApplicationFactoryTests(WebApplicationFactory<Startup> factory) => _factory = factory;

    [Theory]
    [InlineData("/API")]
    public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
    {
        HttpClient client = _factory.CreateClient();
        HttpResponseMessage response = await client.GetAsync(url);

        response.EnsureSuccessStatusCode();

        if (response.Content.Headers.ContentType != null)
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
    }
}
