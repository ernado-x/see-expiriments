using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API.Application
{
    public class Channel
    {
        public Guid Id { get; set; }

        private readonly HttpResponse _response;
        private readonly JsonSerializerSettings _serializerSettings;

        public Channel(Guid channelId, HttpResponse response)
        {
            Id = channelId;

            _response = response;

            _serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        public async Task Send(Event e)
        {
            var json = JsonConvert.SerializeObject(e.Data, _serializerSettings);

            await _response.WriteAsync($"event: {e.ClientId}\n");
            await _response.WriteAsync($"data: {json}\n\n");

            _response.Body.Flush();
        }

        public override string ToString()
        {
            return $"{Id}";
        }
    }
}