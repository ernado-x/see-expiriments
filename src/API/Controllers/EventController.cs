using System;
using System.Linq;
using System.Threading.Tasks;
using API.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("/api/sse")]
    public class ServerSentEventController
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ServerSentEventController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task Get([FromQuery]int clientId)
        {
            var response = _httpContextAccessor.HttpContext.Response;
            response.Headers.Add("Content-Type", "text/event-stream");

            while (true)
            {
                var messages = MessageManager.Current.GetMessages(clientId);

                if (messages.Any())
                {
                    foreach (var m in messages)
                    {
                        await response.WriteAsync($"data: [{m}] at {DateTime.Now}\r\r");
                        response.Body.Flush();
                    }
                }
            }
        }
    }
}