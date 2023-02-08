using ChatWebApi.Models;

namespace ChatWebApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Dictionary<int, User> UsersDB = new()
        {
            {1, new User {userId = 1, username = "aaa", password = "111", isActive = false, roomId = new List<string>() } },
            {2, new User {userId = 2, username = "bbb", password = "222", isActive = false, roomId = new List<string>() } }
        };

        public void AddNewUser(User user)
        {
            user.userId = UsersDB.Count + 1;
            user.roomId = new List<string>();
            UsersDB.TryAdd(user.userId, user);
        }

        public IEnumerable<User> GetAllUsers() => UsersDB.Values.ToList();

        public User GetUserById(int id) => UsersDB.Values.FirstOrDefault(u => u.userId == id);


        public User GetUserByUsername(string username) => UsersDB.Values.FirstOrDefault(u => u.username == username);
    }
}
