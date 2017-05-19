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
        private readonly ConcurrentDictionary<Guid, Channel> _channels;

        public MessageManager()
        {
            _channels = new ConcurrentDictionary<Guid, Channel>();
            _subscriptions = new ConcurrentBag<Subscription>();
        }

        public async Task SendDataToClient(int clientId, object data)
        {
            var e = Event.CreateEvent(clientId, data);

            var subscriptions = _subscriptions.Where(o => o.ClientId == clientId).ToList();

            foreach (var subscription in subscriptions)
            {
                var channel = _channels
                    .Where(c => c.Key == subscription.ChannelId)
                    .Select(o => o.Value)
                    .SingleOrDefault();

                await channel.Send(e);
            }
        }

        public void RegisterChannel(Guid channelId, HttpResponse response)
        {
            var channel = _channels.FirstOrDefault(o => o.Key == channelId);


            if (_channels.ContainsKey(channelId))
            {
                _channels[channelId] = new Channel(channelId, response);
            }
            else
            {
                _channels.TryAdd(channelId, new Channel(channelId, response));
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