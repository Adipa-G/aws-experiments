using System;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HealthAPI.Controllers
{
    [Route("/api/health")]
    public class HealthController
    {
        private readonly IAmazonDynamoDB _dynamoDb;
        private readonly ILogger<HealthController> _logger;

        public HealthController(IAmazonDynamoDB dynamoDb,
            ILogger<HealthController> logger)
        {
            _dynamoDb = dynamoDb;
            _logger = logger;
        }

        [Route("")]
        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            var dbHealthy = false;
            try
            {

                var tables = await _dynamoDb.ListTablesAsync();
                dbHealthy = tables.TableNames.Count >= 0;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Unable to connect to the database");
            }

            return new ObjectResult(new { API = true, DB = dbHealthy });
        }
    }
}
