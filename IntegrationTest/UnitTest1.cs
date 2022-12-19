using Microsoft.AspNetCore.Mvc.Testing;

namespace IntegrationTest
{
    public class UnitTest1 : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public UnitTest1(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/Index")]
        [InlineData("/Index1")]
        [InlineData("/Logout")]
        [InlineData("/Privacy")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            //var client = _factory.CreateClient();

            // Act
            var response = await _client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }
    }
}