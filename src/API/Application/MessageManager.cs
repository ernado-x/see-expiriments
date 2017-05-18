using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        //private readonly List<Subscription> _subscriptions;
        private readonly ConcurrentBag<Subscription> _subscriptions;
        // private readonly List<Channel> _channels;
        private readonly ConcurrentBag<Channel> _channels;

        public MessageManager()
        {
            //_channels = new List<Channel>();
            _channels = new ConcurrentBag<Channel>();
            //_subscriptions = new List<Subscription>();
            _subscriptions = new ConcurrentBag<Subscription>();
        }

        public void SendDataToClient(int clientId, object data)
        {
            var e = new Event(clientId, data);

            var subscriptions = _subscriptions.Where(o => o.ClientId == clientId).ToList();

            foreach (var s in subscriptions)
            {
                var channel = _channels.SingleOrDefault(o => o.Id == s.ChannelId);
                channel.SendToChannel(e);
            }
        }

        public void RegisterChannel(Guid channelId, Action<Event> action)
        {
            var exist = _channels.ToList().Any(o => o.Id == channelId);
            
            if (!exist)
            {
                _channels.Add(new Channel(channelId, action));
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