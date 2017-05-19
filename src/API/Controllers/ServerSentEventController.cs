using System;
using System.Linq;
using System.Threading.Tasks;
using API.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API.Controllers
{
    [Route("/api/sse")]
    public class ServerSentEventController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ServerSentEventController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> RegisterChannel([FromQuery]Guid channelId)
        {
            var response = _httpContextAccessor.HttpContext.Response;
            response.Headers.Add("Content-Type", "text/event-stream");

            MessageManager.Current.RegisterChannel(channelId, response);

            while (true)
            {
                await Task.Delay(100);
            }

            return Ok();
        }

        private static Task WriteSseEventFieldAsync(HttpResponse response, string field, string data)
        {
            return response.WriteAsync($"{field}: {data}\n\n");
        }


        [HttpGet]
        [Route("subscribe")]
        public IActionResult SubscribeClientToChannel([FromQuery]Guid channelId, [FromQuery]int clientId)
        {
            MessageManager.Current.SubscribeClientToChannel(channelId, clientId);

            return Content("OK");
        }

        [HttpGet]
        [Route("version")]
        public IActionResult GetVersion()
        {
            return Content("2.0.2");
        }
    }
}