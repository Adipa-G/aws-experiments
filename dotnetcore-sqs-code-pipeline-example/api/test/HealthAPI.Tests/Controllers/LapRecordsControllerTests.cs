using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;
using FluentAssertions;
using HealthAPI.Config;
using HealthAPI.Controllers;
using HealthAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace HealthAPI.Tests.Controllers
{
    public class LapRecordsControllerTests
    {
        private readonly ApiConfig _apiConfig;
        private readonly IAmazonSQS _sqs;
        private readonly ILogger<LapRecordsController> _logger;

        public LapRecordsControllerTests()
        {
            _apiConfig = new ApiConfig() { QueueName = "test" };
            _sqs = Substitute.For<IAmazonSQS>();
            _logger = Substitute.For<ILogger<LapRecordsController>>();
        }

        [Fact]
        public async Task GivenRecord_WhenAddAsync_ThenSend()
        {
            var queueUrl = "xyz";
            _sqs.GetQueueUrlAsync(_apiConfig.QueueName)
                .Returns(Task.FromResult(new GetQueueUrlResponse() { QueueUrl = queueUrl }));

            var record = new LapRecord();

            var sut = CreateSut();
            var response = await sut.AddAsync(record) as StatusCodeResult;

            response.Should().NotBeNull();
            response?.StatusCode.Should().Be(StatusCodes.Status200OK);
            await _sqs.Received(1).GetQueueUrlAsync(_apiConfig.QueueName);
            await _sqs.Received(1).SendMessageAsync(queueUrl, Arg.Any<string>());
        }

        [Fact]
        public async Task GivenQueueThrowsError_WhenAddAsync_ThenReturnError()
        {
            var queueUrl = "xyz";
            _sqs.GetQueueUrlAsync(_apiConfig.QueueName)
                .Returns(Task.FromResult(new GetQueueUrlResponse() { QueueUrl = queueUrl }));
            _sqs.SendMessageAsync(queueUrl, Arg.Any<string>()).Throws(new Exception());

            var record = new LapRecord();

            var sut = CreateSut();
            var response = await sut.AddAsync(record) as StatusCodeResult;

            response.Should().NotBeNull();
            response?.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }

        private LapRecordsController CreateSut()
        {
            return new LapRecordsController(_apiConfig, _sqs, _logger);
        }
    }
}
