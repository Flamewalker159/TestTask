using System.Net;

namespace TestTask.Tests.Integration;

public class FilreImportTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public FilreImportTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Import_ValidFile_ReturnsCreated()
    {
        //Arrange
        using var content = new MultipartFormDataContent();

        using var fileContent = new StreamContent(File.OpenRead("TestData/valid.csv"));

        content.Add(fileContent, "file", "valid.csv");

        //Act
        var response = await _client.PostAsync("api/file-imports", content);

        //Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task Import_EmptyFile_ReturnsBadRequest()
    {
        //Arrange
        using var content = new MultipartFormDataContent();

        using var fileContent = new StreamContent(File.OpenRead("TestData/empty-file.csv"));

        content.Add(fileContent, "file", "empty-file.csv");

        //Act
        var response = await _client.PostAsync("api/file-imports", content);

        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Import_NoFile_ReturnsBadRequest()
    {
        //Arrange
        using var content = new MultipartFormDataContent();

        //Act
        var response = await _client.PostAsync("/api/file-imports", content);

        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Import_MoreThan10000Records_ReturnsBadRequest()
    {
        //Arrange
        using var content = new MultipartFormDataContent();

        using var fileContent = new StreamContent(File.OpenRead("TestData/10002-rows.csv"));

        content.Add(fileContent, "file", "10002-rows.csv");

        //Act
        var response = await _client.PostAsync("api/file-imports", content);

        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Import_NegativeExecutionTime_ReturnsBadRequest()
    {
        //Arrange
        using var content = new MultipartFormDataContent();

        using var fileContent = new StreamContent(File.OpenRead("TestData/negative-execution-time.csv"));

        content.Add(fileContent, "file", "negative-execution-time.csv");

        //Act
        var response = await _client.PostAsync("api/file-imports", content);

        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Import_NegativeValue_ReturnsBadRequest()
    {
        //Arrange
        using var content = new MultipartFormDataContent();

        using var fileContent = new StreamContent(File.OpenRead("TestData/negative-value.csv"));

        content.Add(fileContent, "file", "negative-value.csv");

        //Act
        var response = await _client.PostAsync("api/file-imports", content);

        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Import_DateBefore2000_ReturnsBadRequest()
    {
        //Arrange
        using var content = new MultipartFormDataContent();

        using var fileContent = new StreamContent(File.OpenRead("TestData/old-date.csv"));

        content.Add(fileContent, "file", "old-date.csv");

        //Act
        var response = await _client.PostAsync("api/file-imports", content);

        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Import_FutureDate_ReturnsBadRequest()
    {
        //Arrange
        using var content = new MultipartFormDataContent();

        using var fileContent = new StreamContent(File.OpenRead("TestData/future-date.csv"));

        content.Add(fileContent, "file", "future-date.csv");

        //Act
        var response = await _client.PostAsync("api/file-imports", content);

        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Import_MissingValue_ReturnsBadRequest()
    {
        //Arrange
        using var content = new MultipartFormDataContent();

        using var fileContent = new StreamContent(File.OpenRead("TestData/missing-value.csv"));

        content.Add(fileContent, "file", "missing-value.csv");

        //Act
        var response = await _client.PostAsync("api/file-imports", content);

        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}