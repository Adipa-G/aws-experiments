using Microsoft.AspNetCore.Mvc;

namespace HealthAPI.Controllers
{
    [Route("/api/health")]
    public class HealthController
    {
        [Route("")]
        [HttpGet]
        public ActionResult Index()
        {
            return new ObjectResult(new { Healty = true });
        }
    }
}
