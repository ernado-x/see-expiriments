using System;
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

        private List<Subscription> _subscriptions;
        private List<Channel> _channels;

        public MessageManager()
        {
            _channels = new List<Channel>();
            _subscriptions = new List<Subscription>();
        }

        public void SendMessageToClient(int clientId, string message)
        {
            var e = new Event
            {
                ClientId = clientId,
                Data = message
            };

            var subscriptions = _subscriptions.Where(o => o.ClientId == clientId).ToList();

            foreach (var s in subscriptions)
            {
                var channel = _channels.SingleOrDefault( o => o.Id == s.ChannelId);
                channel.SendToChannel(e);
            }
        }

        public void RegisterChannel(Guid channelId, Action<Event> action)
        {
            _channels.Add(new Channel(channelId, action));
        }

        public void SubscribeClientToChannel(Guid channelId, int clientId)
        {
            _subscriptions.Add(new Subscription(channelId, clientId));
        }
    }
}