using System;
using System.Threading.Tasks;
using HealthAPI.Model;
using HealthAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HealthAPI.Controllers
{
    [Route("/api/lapRecords")]
    public class LapRecordsController
    {
        private readonly ILapRecordRepository _lapRecordRepository;
        private readonly ILogger<LapRecordsController> _logger;

        public LapRecordsController(ILapRecordRepository lapRecordRepository,
            ILogger<LapRecordsController> logger)
        {
            _lapRecordRepository = lapRecordRepository;
            _logger = logger;
        }

        [Route("")]
        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            try
            {
                var results = await _lapRecordRepository.GetAll();
                return new ObjectResult(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Unable to retrieve records.");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [Route("")]
        [HttpPost]
        public async Task<ActionResult> AddAsync([FromBody] LapRecord record)
        {
            try
            {
                await _lapRecordRepository.Save(record);
                return new OkResult();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Unable to update records");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
