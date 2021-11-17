using System.Runtime.InteropServices.ComTypes;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Order;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Template.API.Application.Tests
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn()]
    public class PerformanceBenchmarks
    {
        [Benchmark]
        public async Task BenchmarkAPIEndpoint()
        {
            var factory = new WebApplicationFactory<Startup>();
            HttpClient client = factory.CreateClient();
            HttpResponseMessage response = await client.GetAsync("/API");

            response.EnsureSuccessStatusCode();
        }
    }
}
