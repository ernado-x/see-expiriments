using System;

namespace API.Application
{
    class Subscription
    {
        public Subscription(Guid channelId, int clientId)
        {
            ChannelId = channelId;
            ClientId = clientId;
        }

        public int ClientId { get; set; }
        public Guid ChannelId { get; set; }

    }
}