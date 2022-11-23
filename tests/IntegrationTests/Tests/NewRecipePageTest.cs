using AngleSharp.Html.Dom;
using Cheaper_Effort.Data;
using Cheaper_Effort.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using RazorPagesProject.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace tests.IntegrationTests.Tests
{
    public class NewRecipePageTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Program> _factory;

        public NewRecipePageTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task Post_AddNewRecipeToDb_ReturnsRedirectToRecipePage()
         {
             Guid guid = new Guid();

            // Act

             var response = await _client.PostAsync("/NewRecipe", new StringContent(
                 JsonConvert.SerializeObject(
                     new Recipe()
             {
                         Id = guid,
                         Name = "whatever",
                         Points = 5,
                         Instructions = "whatever"
             }), Encoding.UTF8, "application/json"));

             // Assert

             response.EnsureSuccessStatusCode(); // Status Code 200-299
             Assert.Equal("text/html; charset=utf-8",
                 response.Content.Headers.ContentType.ToString());
         }

        /*public async Task Post_AddNewRecipeToDb_ReturnsRedirectToRecipePage()
        {

            DbContextOptionsBuilder<ProjectDbContext> optionsBuilder = new();
            optionsBuilder.UseInMemoryDatabase(MethodBase.GetCurrentMethod().Name);

            using (ProjectDbContext ctx = new(optionsBuilder.Options))
            {


                Guid guid = new Guid();


                var request = new HttpRequestMessage(HttpMethod.Post, "/NewRecipe");

                *//* request.Content = new StringContent(JsonSerializer.Serialize(
                 JsonConvert.SerializeObject(new Recipe()
                 {
                     Id = guid,
                     Name = "whatever",
                     Points = 5,
                     Instructions = "whatever"
                 }), Encoding.UTF8, "application/json"));*//*

                request.Content = new StringContent(
                     JsonConvert.SerializeObject(
                         new Recipe()
                         {
                             Name = "whatever",
                             Points = 5,
                             Instructions = "whatever"
                         }), Encoding.UTF8, "application/json");

                var response = await _client.SendAsync(request);

                // Assert

                response.EnsureSuccessStatusCode(); // Status Code 200-299
                Assert.Equal("text/html; charset=utf-8",
                    response.Content.Headers.ContentType.ToString());

            }

        }*/
    }
}
