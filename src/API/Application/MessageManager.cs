using System;
using System.Collections.Generic;
using System.Linq;

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


        private List<Event> _events;

        public MessageManager()
        {
            _events = new List<Event>();
        }

        public void AddMessage(int clientId, string message)
        {
            _events.Add(new Event(clientId, message));
        }

        public IEnumerable<string> GetMessages(int clientId)
        {
            var events = _events.Where(o => o.ClientId == clientId).ToList();

            foreach (var e in events)
            {
                _events.Remove(e);
            }

            return events.Select(o => o.Data).ToList();
        }
    }

    class Event
    {
        public Event(int clientId, string data)
        {
            ClientId = clientId;
            Data = data;
        }

        public int ClientId { get; set; }
        public string Data { get; set; }
    }
}