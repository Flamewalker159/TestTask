using System.Net;
using Newtonsoft.Json;
using TestTask.DTOs;

namespace TestTask.Tests.Integration;

public class ResultsTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public ResultsTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetResults_ByFileName_ReturnsOk()
    {
        // Arrange
        await ImportFileAsync("valid.csv");

        // Act
        var response = await _client.GetAsync("/api/results?FileName=valid.csv");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var body = await response.Content.ReadAsStringAsync();

        var results = JsonConvert.DeserializeObject<List<ResultDto>>(body);

        Assert.NotNull(results);
        Assert.Single(results);
        Assert.Equal("valid.csv", results[0].FileName);
    }

    [Fact]
    public async Task GetResults_FileNotFound_ReturnsNotFound()
    {
        // Act
        var response = await _client.GetAsync("/api/results?fileName=does-not-exist.csv");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetResults_NoFilters_ReturnsAllResults()
    {
        // Arrange
        await ImportFileAsync("valid.csv");
        await ImportFileAsync("valid2.csv");

        // Act
        var response = await _client.GetAsync("/api/results");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var body = await response.Content.ReadAsStringAsync();

        var results = JsonConvert.DeserializeObject<List<ResultDto>>(body);

        Assert.NotNull(results);
        Assert.Contains(results, result => result.FileName == "valid.csv");
        Assert.Contains(results, result => result.FileName == "valid2.csv");
    }

    [Fact]
    public async Task GetResults_ByAverageValue_ReturnsOk()
    {
        // Arrange
        await ImportFileAsync("valid.csv");

        // Act
        var response = await _client.GetAsync("/api/results?averageValueFrom=0");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var body = await response.Content.ReadAsStringAsync();

        var results = JsonConvert.DeserializeObject<List<ResultDto>>(body);

        Assert.NotNull(results);
        Assert.NotEmpty(results);
    }

    [Fact]
    public async Task GetResults_ByStartDate_ReturnsOk()
    {
        // Arrange
        await ImportFileAsync("valid.csv");

        // Act
        var response = await _client.GetAsync("/api/results?startDateFrom=2000-01-01T00:00:00Z");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var body = await response.Content.ReadAsStringAsync();

        var results = JsonConvert.DeserializeObject<List<ResultDto>>(body);

        Assert.NotNull(results);
        Assert.NotEmpty(results);
    }

    private async Task ImportFileAsync(string fileName)
    {
        using var content = new MultipartFormDataContent();

        await using var stream = File.OpenRead($"TestData/{fileName}");

        using var fileContent = new StreamContent(stream);

        content.Add(fileContent, "file", fileName);

        var response = await _client.PostAsync("/api/file-imports", content);

        response.EnsureSuccessStatusCode();
    }
}