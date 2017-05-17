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
        // GET api/values
        [HttpGet]
        public int Get([FromQuery]int clientId, [FromQuery]string message)
        {
            MessageManager.Current.AddMessage(clientId, message);
            return 0;
        }
    }
}
