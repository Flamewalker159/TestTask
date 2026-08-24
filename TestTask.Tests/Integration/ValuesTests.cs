using System.Net;
using System.Net.Http.Json;
using TestTask.Entities;

namespace TestTask.Tests.Integration;

public class ValuesTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public ValuesTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetValues_ExistingFile_ReturnsOk()
    {
        // Arrange
        await ImportFileAsync("valid.csv");

        // Act
        var response = await _client.GetAsync("/api/values/valid.csv");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var values = await response.Content.ReadFromJsonAsync<List<Values>>();

        Assert.NotNull(values);
        Assert.NotEmpty(values);
        Assert.True(values.Count <= 10);
        for (var i = 1; i < values.Count; i++)
            Assert.True(values[i - 1].Date >= values[i].Date);
    }

    [Fact]
    public async Task GetValues_FileNotFound_ReturnsNotFound()
    {
        // Act
        var response = await _client.GetAsync("/api/values/does-not-exist.csv");
        
        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
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