namespace API.Application
{
    public class Event
    {
        private static int _id = 1;

        public static Event CreateEvent(int clientId, object data)
        {
            return new Event
            {
                Id = _id++,
                ClientId = clientId,
                Data = data
            };
        }

        public int Id { get; set; }
        public int ClientId { get; set; }
        public object Data { get; set; }

        private Event()
        {
        }


        public override string ToString()
        {
            return $"Client: {ClientId}  Data: {Data}";
        }
    }
}