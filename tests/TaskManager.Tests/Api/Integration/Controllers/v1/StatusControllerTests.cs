using FluentAssertions;
using Newtonsoft.Json;
using System.Net;
using TaskManager.Application.Features.Status.Responses;
using TaskManager.Tests.Helpers;

namespace TaskManager.Tests.Api.Integration.Controllers.v1;

public class StatusControllerTests : IntegrationTestBase<Program>
{
    private readonly HttpClient _client;

    public StatusControllerTests()
    {
        _client = GetTestAppClient();
    }

    [Fact(DisplayName = "Should return status 'Ok' when get all status valid")]
    public async Task GetAllStatusbBy_GetAllAStatusValid_ShouldReturnStatusIsValid()
    {
        //Act
        var response = await _client.GetAsync($"/v1/status");

        //Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsStringAsync();

        var statusResponse = JsonConvert.DeserializeObject<IEnumerable<StatusResponse>>(content);

        statusResponse.Should().NotBeNull();
    }
}