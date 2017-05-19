using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace API.Application
{
    public class MessageManager
    {
        private static MessageManager _instance;
        public static MessageManager Current
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MessageManager();
                }

                return _instance;
            }
        }

        private readonly ConcurrentBag<Subscription> _subscriptions;
        private readonly ConcurrentBag<Channel> _channels;

        private HttpResponse _response;

        public MessageManager()
        {
            _channels = new ConcurrentBag<Channel>();
            _subscriptions = new ConcurrentBag<Subscription>();
        }

        public async Task SendDataToClient(int clientId, object data)
        {
            var e = Event.CreateEvent(clientId, data);

            var subscriptions = _subscriptions.Where(o => o.ClientId == clientId).ToList();

            foreach (var subscription in subscriptions)
            {
                var channel = _channels.SingleOrDefault(c => c.Id == subscription.ChannelId);
                await channel.Send(e);
            }
        }

        public void RegisterChannel(Guid channelId, HttpResponse response)
        {
            var exist = _channels.Any(o => o.Id == channelId);

            if (!exist)
            {
                _channels.Add(new Channel(channelId, response));
            }
        }

        public void SubscribeClientToChannel(Guid channelId, int clientId)
        {
            var exist = _subscriptions.Any(o => o.ChannelId == channelId && o.ClientId == clientId);

            if (!exist)
            {
                _subscriptions.Add(new Subscription(channelId, clientId));
            }
        }
    }
}