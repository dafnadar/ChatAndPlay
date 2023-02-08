namespace ChatWebApi.Models
{
    public class User
    {
        public int userId { get; set; }
        public string? username { get; set; }
        public string? password { get; set; }
        public bool? isActive { get; set; }
        public List<string>? roomId { get; set; }

    }
}
