using System;

namespace API.Application
{
    public class Channel
    {
        public Guid Id { get; set; }

        public readonly Action<Event> SendToChannel;

        public Channel(Guid channelId, Action<Event> action)
        {
            Id = channelId;
            SendToChannel = action;
        }

        public override string ToString()
        {
            return $"{Id}";
        }
    }
}