using FluentAssertions;
using HealthAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace HealthAPI.Tests.Controllers
{
    public class HealthControllerTests
    {
        [Fact]
        public void GivenHealthController_WhenIndex_ThenReturnResponse()
        {
            var sut = CreateSut();

            var response = sut.Index() as ObjectResult;

            response.Should().NotBeNull();
            response?.Value.Should().BeEquivalentTo(new { Healty = true });
        }

        private HealthController CreateSut()
        {
            return new HealthController();
        }
    }
}
