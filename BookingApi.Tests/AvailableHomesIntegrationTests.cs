using System.Net;
using System.Net.Http.Json;
using BookingAPI.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using BookingAPI;


namespace BookingAPI.Tests
{
    public class AvailableHomesIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public AvailableHomesIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAvailableHomes_ShouldReturnBadRequest_WhenEndDateIsBeforeStartDate()
        {
            var startDate = "2025-07-20";
            var endDate = "2025-07-15"; // invalid if(end < start)
            var url = $"/api/available-homes?startDate={startDate}&endDate={endDate}";

            var response = await _client.GetAsync(url);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetAvailableHomes_ShouldReturnHomes_WhenDatesMatch()
        {
            var startDate = "2025-07-15";
            var endDate = "2025-07-16";
            var url = $"/api/available-homes?startDate={startDate}&endDate={endDate}";

            var response = await _client.GetAsync(url);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await response.Content.ReadFromJsonAsync<AvailableHomesResponse>();
            result.Should().NotBeNull();
            result!.Status.Should().Be("OK");

            // Home 1 has slots on 15 and 16, so it should appear
            result.Homes.Should().ContainSingle(h => h.HomeId == "123");
        }

        [Fact]
        public async Task GetAvailableHomes_ShouldReturnEmpty_WhenNoHomesMatch()
        {
            var startDate = "2025-07-20";
            var endDate = "2025-07-21";
            var url = $"/api/available-homes?startDate={startDate}&endDate={endDate}";

            var response = await _client.GetAsync(url);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await response.Content.ReadFromJsonAsync<AvailableHomesResponse>();
            result.Should().NotBeNull();
            result!.Homes.Should().BeEmpty();
        }
    }
}

