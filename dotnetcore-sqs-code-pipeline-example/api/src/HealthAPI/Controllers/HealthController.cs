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
        private readonly IAmazonSQS _amazonSqs;
        private readonly ILogger<HealthController> _logger;
        private readonly Config.ApiConfig _apiConfig;

        public HealthController(IAmazonSQS amazonSqs,
            ILogger<HealthController> logger,
            Config.ApiConfig apiConfig)
        {
            _amazonSqs = amazonSqs;
            _logger = logger;
            _apiConfig = apiConfig;
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
