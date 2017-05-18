namespace API.Application
{
    public class Event
    {
        public int ClientId { get; set; }
        public object Data { get; set; }

        public Event()
        {
        }

        public Event(int clientId, object data)
        {
            ClientId = clientId;
            Data = data;
        }        

        public override string ToString()
        {
            return $"Client: {ClientId}  Data: {Data}";
        }
    }
}