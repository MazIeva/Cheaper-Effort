using AngleSharp.Html.Dom;
using AngleSharp.Io;
using Cheaper_Effort.Data;
using Cheaper_Effort.Serivces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RazorPagesProject.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using tests.IntegrationTests.Helpers;

namespace tests.IntegrationTests
{
    public class RecipeDetailsTest : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;
        public RecipeDetailsTest(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task Post_DeleteById_ReturnsRedirectToRecipePage()
        {
            // Arrange

            var client = _factory.WithWebHostBuilder(builder =>
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
                    services.AddScoped<IRecipeService, TestRecipeService>();
                    services.AddScoped<INewRecipeService, TestNewRecipeService>();
                });
            })
            .CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });


            // Act

            /*var recipeDeletePage = await client.GetAsync("/RecipePages/DeleteRecipe/?id=3a577e39-5758-4de5-b3a1-3000a9a6db1f");
            var content = await HtmlHelpers.GetDocumentAsync(recipeDeletePage);
            var buttonElement = content.QuerySelector("#delete");
            var formElement = content.QuerySelector("form[id='deleteForm']");
            IEnumerable<KeyValuePair<string, string>> keyValuePair = new Dictionary<string, string>()
                                                                            {
                                                                                {"data-arg-id", "3a577e39-5758-4de5-b3a1-3000a9a6db1f"}
                                                                            };
            var response = await _client.SendAsync(
                (IHtmlFormElement) formElement,
                (IHtmlButtonElement) buttonElement
                //keyValuePair
                );*/

            var response = await _client.DeleteAsync(
                "/api/RecipeController/DeleteRecipe?id=3a577e39-5758-4de5-b3a1-3000a9a6db1f");


            Console.WriteLine("123");

            // Assert
           // client.

        }
    }
}
