using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using HealthAPI.Controllers;
using HealthAPI.Model;
using HealthAPI.Repositories;
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
        private readonly ILapRecordRepository _repository;
        private readonly ILogger<LapRecordsController> _logger;

        public LapRecordsControllerTests()
        {
            _repository = Substitute.For<ILapRecordRepository>();
            _logger = Substitute.For<ILogger<LapRecordsController>>();
        }

        [Fact]
        public async Task GivenRepoReturnsResults_WhenGetAsync_ThenReturnResults()
        {
            var result = new List<LapRecord>() { new LapRecord() };
            _repository.GetAll().Returns(result);

            var sut = CreateSut();
            var response = await sut.GetAsync() as ObjectResult;

            response.Should().NotBeNull();
            response?.Value.Should().BeEquivalentTo(result);
        }

        [Fact]
        public async Task GivenRepoThrowsAnError_WhenGetAsync_ThenReturnErrorResult()
        {
            _repository.GetAll().Throws(new Exception());

            var sut = CreateSut();
            var response = await sut.GetAsync() as StatusCodeResult;

            response.Should().NotBeNull();
            response?.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }

        [Fact]
        public async Task GivenRecord_WhenAddAsync_ThenSave()
        {
            var record = new LapRecord();

            var sut = CreateSut();
            var response = await sut.AddAsync(record) as StatusCodeResult;

            response.Should().NotBeNull();
            response?.StatusCode.Should().Be(StatusCodes.Status200OK);
            await _repository.Received(1).Save(record);
        }

        private LapRecordsController CreateSut()
        {
            return new LapRecordsController(_repository, _logger);
        }
    }
}
