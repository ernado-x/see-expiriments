using System;

namespace API.Application
{
    class Subscription
    {
        public int ClientId { get; set; }
        public Guid ChannelId { get; set; }

        public Subscription(Guid channelId, int clientId)
        {
            ChannelId = channelId;
            ClientId = clientId;
        }
        
        public override string ToString()
        {
            return $"ChannelId: {ChannelId}  ClientId: {ClientId}";
        }
    }
}