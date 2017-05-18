using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Application;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/messages")]
    public class MessagesController : Controller
    {
        [HttpGet]
        [Route("send")]
        public async Task<string> SendMessageToClient([FromQuery]int clientId, [FromQuery]string message)
        {
            MessageManager.Current.SendMessageToClient(clientId, message);

            return await Task.FromResult($"Message sent to {clientId}");
        }
    }
}
