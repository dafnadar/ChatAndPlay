namespace ChatWebApi.Models
{
    public class Message
    {
        public int id { get; set; }
        public string? sender { get; set; }
        public string? textMsg { get; set; }
        public string? room { get; set; }
        public DateTime time { get; set; }

    }
}
