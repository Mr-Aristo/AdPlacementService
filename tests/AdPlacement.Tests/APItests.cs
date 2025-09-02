using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace AdPlacement.Tests;

public class APItests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public APItests(WebApplicationFactory<Program> factory)
    {

        _client = factory.CreateClient();
    }

    [Fact]
    public async Task UploadPlacements_ShouldReturnOk()
    {

        var content = "Yandex:/ru\nLocalAds:/ru/msk,/ru/spb";

        var stringContent = new StringContent(content, Encoding.UTF8, "text/plain");


        var response = await _client.PostAsync("/api/platforms/upload", stringContent);


        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetPlatforms_ShouldReturnPlatforms()
    {

        var content = "Yandex:/ru\nLocalAds:/ru/msk,/ru/spb";
        var stringContent = new StringContent(content, Encoding.UTF8, "text/plain");
        await _client.PostAsync("/api/platforms/upload", stringContent);


        var response = await _client.GetAsync("/api/platforms?location=/ru/msk");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<List<string>>();


        result.Should().NotBeNull();
        result.Should().Contain("LocalAds");
        result.Should().Contain("Yandex");
    }

    [Fact]
    public async Task HealthEndpoints_ShouldReturnHealthy()
    {
        var live = await _client.GetAsync("/health/live");
        live.StatusCode.Should().Be(HttpStatusCode.OK);

        var ready = await _client.GetAsync("/health/ready");
        ready.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
