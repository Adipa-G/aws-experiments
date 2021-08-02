using System;
using System.Threading.Tasks;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using HealthAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HealthAPI.Controllers
{
    [Route("/api/messages")]
    public class MessagesController
    {
        private readonly IAmazonSimpleNotificationService _amazonSns;
        private readonly ILogger<MessagesController> _logger;

        public MessagesController(IAmazonSimpleNotificationService amazonSns,
            ILogger<MessagesController> logger)
        {
            _amazonSns = amazonSns;
            _logger = logger;
        }

        [Route("")]
        [HttpPost]
        public async Task<ActionResult> AddAsync([FromBody] Message msg)
        {
            try
            {
                var response = await _amazonSns.PublishAsync(new PublishRequest() { Message = msg.Text, PhoneNumber = msg.PhoneNumber });
                return new StatusCodeResult((int)response.HttpStatusCode);
                ;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Unable to send message");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
