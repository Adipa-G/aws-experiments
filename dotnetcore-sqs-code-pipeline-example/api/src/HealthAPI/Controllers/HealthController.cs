using System;
using System.Threading.Tasks;
using Amazon.SQS;
using HealthAPI.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HealthAPI.Controllers
{
    [Route("/api/health")]
    public class HealthController
    {
        private readonly ApiConfig _apiConfig;
        private readonly IAmazonSQS _amazonSqs;
        private readonly ILogger<HealthController> _logger;

        public HealthController(ApiConfig apiConfig,
            IAmazonSQS amazonSqs,
            ILogger<HealthController> logger)
        {
            _apiConfig = apiConfig;
            _amazonSqs = amazonSqs;
            _logger = logger;
        }

        [Route("")]
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var queueHealthy = false;
            try
            {
                var queueDetails = await _amazonSqs.GetQueueUrlAsync(_apiConfig.QueueName);
                queueHealthy = queueDetails != null;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Unable to connect to the queue");
            }

            return new ObjectResult(new { API = true, Queue = queueHealthy });
        }
    }
}
