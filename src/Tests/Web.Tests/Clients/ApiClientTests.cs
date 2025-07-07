using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using Contracts;
using Moq;
using Moq.Protected;
using Shared;
using UI.Clients;
using Xunit;

namespace Web.Tests.Clients;

public class ApiClientTests
{
    private readonly JsonSerializerOptions _jsonOptions = new(JsonSerializerDefaults.Web);

    private HttpClient CreateHttpClientWithResponse(HttpResponseMessage responseMessage)
    {
        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseMessage);

        return new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("https://localhost/")
        };
    }

    [Fact]
    public async Task RegisterAsync_Returns_True_When_Successful()
    {
        var expected = true;
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = JsonContent.Create(expected, options: _jsonOptions)
        };
        var httpClient = CreateHttpClientWithResponse(response);
        var apiClient = new ApiClient(httpClient);

        var result = await apiClient.RegisterAsync(new RegistrationDTO());

        Assert.True(result);
    }

    [Fact]
    public async Task LoginAsync_Returns_True_When_Successful()
    {
        var expected = true;
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = JsonContent.Create(expected, options: _jsonOptions)
        };
        var httpClient = CreateHttpClientWithResponse(response);
        var apiClient = new ApiClient(httpClient);

        var result = await apiClient.LoginAsync(new AuthenticationDTO() { Email = "test@test.com", Password = "password"});

        Assert.True(result);
    }

    [Fact]
    public async Task GetAllAsync_Returns_ListOfUsers_When_Successful()
    {
        var expected = new List<UserDTO>
        {
            CreateStubUser(),
            CreateStubUser()
        };

        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = JsonContent.Create(expected, options: _jsonOptions)
        };
        var httpClient = CreateHttpClientWithResponse(response);
        var apiClient = new ApiClient(httpClient);

        var result = await apiClient.GetAllAsync();

        Assert.Equal(expected.Count, result.Count);
        Assert.Equal(expected[0].Id, result[0].Id);
        Assert.Equal(expected[1].Id, result[1].Id);

    }

    [Fact]
    public async Task GetAsync_Returns_User_When_Found()
    {
        var expected = CreateStubUser();

        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = JsonContent.Create(expected, options: _jsonOptions)
        };
        var httpClient = CreateHttpClientWithResponse(response);
        var apiClient = new ApiClient(httpClient);

        var result = await apiClient.GetAsync(expected.Id);

        Assert.NotNull(result);
        Assert.Equal(expected.Id, result!.Id);
    }

    [Fact]
    public async Task GetAsync_Returns_Null_When_NotFound()
    {
        var response = new HttpResponseMessage(HttpStatusCode.NotFound);
        var httpClient = CreateHttpClientWithResponse(response);
        var apiClient = new ApiClient(httpClient);

        var result = await apiClient.GetAsync(Guid.NewGuid());

        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAsync_Returns_True_When_Successful()
    {
        var response = new HttpResponseMessage(HttpStatusCode.Created);
        var httpClient = CreateHttpClientWithResponse(response);
        var apiClient = new ApiClient(httpClient);

        var result = await apiClient.CreateAsync(new UserDTO());

        Assert.True(result);
    }

    [Fact]
    public async Task UpdateAsync_Returns_True_When_Successful()
    {
        var response = new HttpResponseMessage(HttpStatusCode.OK);
        var httpClient = CreateHttpClientWithResponse(response);
        var apiClient = new ApiClient(httpClient);

        var result = await apiClient.UpdateAsync(new UserDTO { Id = Guid.NewGuid() });

        Assert.True(result);
    }

    [Fact]
    public async Task DeleteAsync_Returns_True_When_Successful()
    {
        var response = new HttpResponseMessage(HttpStatusCode.OK);
        var httpClient = CreateHttpClientWithResponse(response);
        var apiClient = new ApiClient(httpClient);

        var result = await apiClient.DeleteAsync(Guid.NewGuid());

        Assert.True(result);
    }

    private static UserDTO CreateStubUser()
    {
        return new UserDTO
        {
            Id = Guid.NewGuid(),
            FirstName = "Jane",
            LastName = "Doe",
            Email = "jane.doe@example.com",
            Phone = "5551234567",
            AddressLine1 = "123 Main St",
            AddressLine2 = "Apt 4B",
            City = "Springfield",
            State = "IL",
            PostalCode = "62704",
            DateOfBirth = new DateTime(1990, 5, 21)
        };
    }
}
