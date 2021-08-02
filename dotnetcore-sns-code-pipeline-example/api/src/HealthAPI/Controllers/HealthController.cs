using System;
using System.Threading.Tasks;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HealthAPI.Controllers
{
    [Route("/api/health")]
    public class HealthController
    {
        private readonly IAmazonSimpleNotificationService _amazonSns;
        private readonly ILogger<HealthController> _logger;

        public HealthController(IAmazonSimpleNotificationService amazonSns,
            ILogger<HealthController> logger)
        {
            _amazonSns = amazonSns;
            _logger = logger;
        }

        [Route("")]
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var snsHealthy = false;
            try
            {
                var smsAttributes = await _amazonSns.GetSMSAttributesAsync(new GetSMSAttributesRequest());
                snsHealthy = smsAttributes != null;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Unable to connect to the simple notification service");
            }

            return new ObjectResult(new { API = true, Sns = snsHealthy });
        }
    }
}
