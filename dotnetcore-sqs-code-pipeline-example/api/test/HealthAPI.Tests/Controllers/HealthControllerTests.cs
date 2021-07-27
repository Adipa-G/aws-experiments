using System;
using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;
using FluentAssertions;
using HealthAPI.Config;
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
        private readonly IAmazonSQS _sqs;
        private readonly ILogger<HealthController> _logger;
        private QueueConfig _queueConfig;

        public HealthControllerTests()
        {
            _sqs = Substitute.For<IAmazonSQS>();
            _logger = Substitute.For<ILogger<HealthController>>();
            _queueConfig = new QueueConfig() { Name = "test" };
        }

        [Fact]
        public async Task GivenHealthy_WhenGetAsync_ThenReturnResponse()
        {
            _sqs.GetQueueUrlAsync(_queueConfig.Name).Returns(Task.FromResult(new GetQueueUrlResponse()));

            var sut = CreateSut();
            var response = await sut.Index() as ObjectResult;

            response.Should().NotBeNull();
            response?.Value.Should().BeEquivalentTo(new { API = true, Queue = true });
        }

        [Fact]
        public async Task GivenNoDbConnectivity_WhenGetAsync_ThenReturnResponse()
        {
            _sqs.GetQueueUrlAsync(_queueConfig.Name).Throws(new Exception());

            var sut = CreateSut();
            var response = await sut.Index() as ObjectResult;

            response.Should().NotBeNull();
            response?.Value.Should().BeEquivalentTo(new { API = true, Queue = false });
        }

        private HealthController CreateSut()
        {
            return new HealthController(_sqs, _logger, _queueConfig);
        }
    }
}
