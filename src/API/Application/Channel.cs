using System;

namespace API.Application
{
    public class Channel
    {
        public Guid Id { get; set; }

        public Action<Event> SendToChannel;

        public Channel(Guid channelId, Action<Event> action)
        {
            Id = channelId;
            SendToChannel = action;
        }
    }
}