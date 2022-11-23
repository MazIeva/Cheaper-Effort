using Cheaper_Effort.Data.Migrations;
using Cheaper_Effort.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace tests.IntegrationTests.Tests
{
    public class RegisterPageTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Program> _factory;

        public RegisterPageTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Post_RegisterAccountSuccess()
        {
            // Arrange

            var account = new Account()
            {
                Id = 489,
                Email = "smetona@gmail.com",
                FirstName = "Antanas",
                LastName = "Smetona",
                Username = "Antanas123",
                Password = "Belenkas123!",
                ConfirmPassword = "Belenkas123!"
            };

            var data = new StringContent(JsonConvert.SerializeObject(account));

            // Act

            var response = await _client.PostAsync("/Register", data);

            // Assert

            response.ShouldNotBeNull();

            response.EnsureSuccessStatusCode();

        }

        [Fact]
        public async Task Post_RegisterAccountFailure()
        {
            // Arrange

            var account = new Account()
            {
                Id = 0,
                Email = "",
                FirstName = "",
                LastName = "",
                Username = "",
                Password = "",
                ConfirmPassword = ""
            };

            var data = new StringContent(JsonConvert.SerializeObject(account));

            // Act

            var response = await _client.PostAsync("/Register", data);

            // Assert

            response.ShouldNotBeNull();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        }

    }
}
