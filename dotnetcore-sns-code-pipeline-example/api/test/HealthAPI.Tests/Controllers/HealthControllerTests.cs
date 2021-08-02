using System;
using System.Threading.Tasks;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
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
        private readonly IAmazonSimpleNotificationService _sns;
        private readonly ILogger<HealthController> _logger;


        public HealthControllerTests()
        {
            _sns = Substitute.For<IAmazonSimpleNotificationService>();
            _logger = Substitute.For<ILogger<HealthController>>();
        }

        [Fact]
        public async Task GivenHealthy_WhenGetAsync_ThenReturnResponse()
        {
            _sns.GetSMSAttributesAsync(Arg.Any<GetSMSAttributesRequest>())
                .Returns(Task.FromResult(new GetSMSAttributesResponse()));

            var sut = CreateSut();
            var response = await sut.Index() as ObjectResult;

            response.Should().NotBeNull();
            response?.Value.Should().BeEquivalentTo(new { API = true, Sns = true });
        }

        [Fact]
        public async Task GivenSnsConnectivity_WhenGetAsync_ThenReturnResponse()
        {
            _sns.GetSMSAttributesAsync(Arg.Any<GetSMSAttributesRequest>()).Throws(new Exception());

            var sut = CreateSut();
            var response = await sut.Index() as ObjectResult;

            response.Should().NotBeNull();
            response?.Value.Should().BeEquivalentTo(new { API = true, Sns = false });
        }

        private HealthController CreateSut()
        {
            return new HealthController(_sns, _logger);
        }
    }
}
