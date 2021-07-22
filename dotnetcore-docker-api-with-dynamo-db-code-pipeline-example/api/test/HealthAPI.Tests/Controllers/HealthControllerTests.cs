using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using FluentAssertions;
using HealthAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace HealthAPI.Tests.Controllers
{
    public class HealthControllerTests
    {
        private readonly IAmazonDynamoDB _dynamoDb;
        private readonly ILogger<HealthController> _logger;

        public HealthControllerTests()
        {
            _dynamoDb = Substitute.For<IAmazonDynamoDB>();
            _logger = Substitute.For<ILogger<HealthController>>();
        }

        [Fact]
        public async Task GivenHealthy_WhenIndex_ThenReturnResponse()
        {
            _dynamoDb.ListTablesAsync().Returns(new ListTablesResponse()
            {
                TableNames = new List<string>()
            });

            var sut = CreateSut();
            var response = await sut.Index() as ObjectResult;

            response.Should().NotBeNull();
            response?.Value.Should().BeEquivalentTo(new { API = true, DB = true });
        }

        [Fact]
        public async Task GivenNoDbConnectivity_WhenIndex_ThenReturnResponse()
        {
            _dynamoDb.ListTablesAsync().Throws(new Exception());

            var sut = CreateSut();
            var response = await sut.Index() as ObjectResult;

            response.Should().NotBeNull();
            response?.Value.Should().BeEquivalentTo(new { API = true, DB = false });
        }

        private HealthController CreateSut()
        {
            return new HealthController(_dynamoDb, _logger);
        }
    }
}
