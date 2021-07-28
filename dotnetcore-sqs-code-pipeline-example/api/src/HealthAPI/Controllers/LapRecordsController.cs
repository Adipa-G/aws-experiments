using System;
using System.Text.Json;
using System.Threading.Tasks;
using Amazon.SQS;
using HealthAPI.Config;
using HealthAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HealthAPI.Controllers
{
    [Route("/api/lapRecords")]
    public class LapRecordsController
    {
        private readonly ApiConfig _apiConfig;
        private readonly IAmazonSQS _amazonSqs;
        private readonly ILogger<LapRecordsController> _logger;

        public LapRecordsController(ApiConfig apiConfig,
            IAmazonSQS amazonSqs,
            ILogger<LapRecordsController> logger)
        {
            _apiConfig = apiConfig;
            _amazonSqs = amazonSqs;
            _logger = logger;
        }

        [Route("")]
        [HttpPost]
        public async Task<ActionResult> AddAsync([FromBody] LapRecord record)
        {
            try
            {
                var queueUrl = await _amazonSqs.GetQueueUrlAsync(_apiConfig.QueueName);
                await _amazonSqs.SendMessageAsync(queueUrl.QueueUrl, JsonSerializer.Serialize(record));
                return new OkResult();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Unable to send message");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
