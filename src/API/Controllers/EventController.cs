using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class EventController : Controller
    {
        // GET api/values
        [HttpGet]
        public bool Get([FromQuery]int clientId, [FromQuery]string message)
        {
            return true;
        }
    }
}