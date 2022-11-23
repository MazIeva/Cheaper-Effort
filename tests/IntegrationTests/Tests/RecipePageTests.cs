using AngleSharp.Html.Dom;
using Cheaper_Effort.Data;
using Cheaper_Effort.Serivces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using RazorPagesProject.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Shouldly;
using System.Diagnostics;
using System.Net.Http.Json;
using Cheaper_Effort.Models;

namespace tests.IntegrationTests.Tests
{
    public class RecipePageTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Program> _factory;

        public RecipePageTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            /*_client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });*/
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Get_RecipeList_IsDisplayed()
        {
            // Arrange


            // Act

            var response = await _client.GetAsync("/Recipes");

            //var content = await HtmlHelpers.GetDocumentAsync(response);

            var responseBody = await response.Content.ReadAsStringAsync();

            // Assert

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(responseBody);

            var recipesElement = htmlDocument.GetElementbyId("DisplayedRecipes");
            recipesElement.ShouldNotBeNull();

            var trNodes = recipesElement.SelectNodes("//tbody/tr");
            trNodes.Count.ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task Post_FilterRecipes_ReturnsRedirectToRecipePage()
        {
            /*// Arrange
            var defaultPage = await _client.GetAsync("/NewRecipe");
            var content = await HtmlHelpers.GetDocumentAsync(defaultPage);

            // Act
            var response = await _client.SendAsync(
                (IHtmlFormElement)content.QuerySelector("form[]"),
                (IHtmlButtonElement)content.QuerySelector("button[id=]")
            );
            // Assert*/

            DbContextOptionsBuilder<ProjectDbContext> optionsBuilder = new();
            optionsBuilder.UseInMemoryDatabase(MethodBase.GetCurrentMethod().Name);

            using (ProjectDbContext ctx = new(optionsBuilder.Options))
            {
                
            }
        }
        [Fact]
        public async Task Get_ReturnsRedirectToNewRecipePage()
        {
            // Arrange
            var defaultPage = await _client.GetAsync("/Recipe");
            var content = await HtmlHelpers.GetDocumentAsync(defaultPage);

            // Act
            var response = await _client.SendAsync(
                (IHtmlFormElement)content.QuerySelector("form[id='newRecipePage']"),
                (IHtmlButtonElement)content.QuerySelector("button[id='newRecipePageBtn']"));

            // Assert

            Assert.Equal(HttpStatusCode.OK, defaultPage.StatusCode);
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.Equal("/Recipe", response.Headers.Location.OriginalString);
        }

    }
}