using System;
using System.Net;
using System.Threading.Tasks;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using FluentAssertions;
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
    public class MessagesControllerTests
    {
        private readonly IAmazonSimpleNotificationService _sns;
        private readonly ILogger<MessagesController> _logger;

        public MessagesControllerTests()
        {
            _sns = Substitute.For<IAmazonSimpleNotificationService>();
            _logger = Substitute.For<ILogger<MessagesController>>();
        }

        [Fact]
        public async Task GivenRecord_WhenAddAsync_ThenSend()
        {
            var msg = new Message() { PhoneNumber = "123", Text = "abc" };
            _sns.PublishAsync(Arg.Any<PublishRequest>())
                .Returns(Task.FromResult(new PublishResponse() { HttpStatusCode = HttpStatusCode.OK }));

            var sut = CreateSut();
            var response = await sut.AddAsync(msg) as StatusCodeResult;

            response.Should().NotBeNull();
            response?.StatusCode.Should().Be(StatusCodes.Status200OK);
            await _sns.Received(1)
                .PublishAsync(Arg.Is<PublishRequest>(r => r.PhoneNumber == msg.PhoneNumber && r.Message == msg.Text));
        }

        [Fact]
        public async Task GivenSnsThrowsError_WhenAddAsync_ThenReturnError()
        {
            var msg = new Message() { PhoneNumber = "123", Text = "abc" };
            _sns.PublishAsync(Arg.Any<PublishRequest>()).Throws(new Exception());

            var sut = CreateSut();
            var response = await sut.AddAsync(msg) as StatusCodeResult;

            response.Should().NotBeNull();
            response?.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }

        private MessagesController CreateSut()
        {
            return new MessagesController(_sns, _logger);
        }
    }
}
