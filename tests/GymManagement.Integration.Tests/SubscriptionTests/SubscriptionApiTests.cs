using System.Net.Http.Json;
using FluentAssertions;
using Flurl;
using Flurl.Http;
using GymManagement.Api;
using GymManagement.Contracts.Subscriptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;

namespace GymManagement.Integration.Tests.SubscriptionTests;

public class SubscriptionApiTests : IClassFixture<SubscriptionApiFactory>
{
    private readonly SubscriptionApiFactory _apiFactory;
    private readonly HttpClient _httpClient;

    public SubscriptionApiTests(SubscriptionApiFactory apiFactory)
    {
        _apiFactory = apiFactory;
        _httpClient = apiFactory.CreateClient();
    }

    [Fact]
    public async Task FirstTest()
    {
        var sub = await _httpClient
        .PostAsJsonAsync(
            "subscriptions", 
            new CreateSubscriptionRequest(SubscriptionType.Free, Guid.NewGuid()));

        sub.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

    }
}