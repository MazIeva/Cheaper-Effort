using Cheaper_Effort.Data;
using Cheaper_Effort.Models;
using Cheaper_Effort.Pages.RecipePages;
using Cheaper_Effort.Serivces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using tests.IntegrationTests.Helpers;
using HttpMethod = System.Net.Http.HttpMethod;

namespace tests.IntegrationTests
{
    public class RecipeDetailsTest : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;
        public RecipeDetailsTest(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var serviceProvider = services.BuildServiceProvider();

                    using (var scope = serviceProvider.CreateScope())
                    {
                        var scopedServices = scope.ServiceProvider;
                        var db = scopedServices
                            .GetRequiredService<ProjectDbContext>();
                        var logger = scopedServices
                            .GetRequiredService<ILogger<RecipeDetailsTest>>();

                        try
                        {
                            Utilities.ReinitializeDbForTests(db);
                        }
                        catch (Exception ex)
                        {
                            logger.LogError(ex, "An error occurred seeding " +
                                "the database with test messages. Error: {Message}",
                                ex.Message);
                        }
                    }
                });
            })
            .CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task Delete_RemovesRecipeById_ReturnsOK()
        {
            // Arrange

            String baseAdress = _client.BaseAddress.ToString();
            Uri uri = new Uri(baseAdress + "Api/Recipe/DeleteRecipe");

            HttpRequestMessage request = new HttpRequestMessage();
            request.Method = HttpMethod.Delete;
            request.RequestUri = uri;

            var data = new[] { new KeyValuePair<String, String>("id", "3a577e39-5758-4de5-b3a1-3000a9a6db1f") };
            request.Content = new FormUrlEncodedContent(data);

            // Act

            var response = await _client.SendAsync(request);
            var assertDeleted = await _client.GetAsync(baseAdress + "RecipePages/RecipeDetails?id=3a577e39-5758-4de5-b3a1-3000a9a6db1f");

            // Assert

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            assertDeleted.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Get_RecipeEdit_ReturnsRedirectToPage()
        {
            // Arrange

            String baseAdress = _client.BaseAddress.ToString();
            Uri uri = new Uri(baseAdress + "RecipePages/Edit");

            HttpRequestMessage request = new HttpRequestMessage();
            request.Method = HttpMethod.Get;
            request.RequestUri = uri;

            var data = new[] { new KeyValuePair<String, String>("id", "3a577e39-5758-4de5-b3a1-3000a9a6db1f") };
            request.Content = new FormUrlEncodedContent(data);

            // Act

            var response = await _client.SendAsync(request);

            // Assert

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
