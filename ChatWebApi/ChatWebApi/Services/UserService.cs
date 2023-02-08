using ChatWebApi.Models;
using ChatWebApi.Repositories;

namespace ChatWebApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public string IsValidUser(User user)
        {
            if (!String.IsNullOrEmpty(user.username) && !String.IsNullOrEmpty(user.password))
            {
                if (user.username.Length > 2 && user.password.Length > 2)
                    return "";
                else
                    return "username & password must be at least 3 characters";
            }
            else
                return "Please enter username and password!";
        }

        public void AddNewUser(User user) => userRepository.AddNewUser(user);

        public string IsNewUser(User user)
        {
            string message = IsValidUser(user);
            if (message == "")
            {
                var newUser = userRepository.GetUserByUsername(user.username!);
                if (newUser == null)
                {
                    AddNewUser(user);
                    return message;
                }
                else
                    return "Username already exists. please choose another username";
            }

            return message;
        }

        public string Login(User user)
        {
            string message = IsValidUser(user);
            if (message == "")
            {
                var userFromDb = userRepository.GetUserByUsername(user.username!);
                if (userFromDb != null)
                {
                    var isPasswordOk = CheckPassword(user.password, userFromDb.password);
                    if (isPasswordOk)
                    {
                        return message;
                    }
                    else
                        return "Please check username or password";
                }
                else
                    return "Username does not exist";

            }

            return message;
        }

        private bool CheckPassword(string? password1, string? password2) => password1 == password2;

        public User GetUserByUsername(string username) => userRepository.GetUserByUsername(username);

        public User GetUserById(int id) => userRepository.GetUserById(id);

        public IEnumerable<User> GetUsersExceptSelf(int id) => userRepository.GetAllUsers().Where(u => u.userId != id);

        public void Logout(int id) => GetUserById(id).isActive = false;

        public User SetActiveUser(User user)
        {
            var userFromDB = userRepository.GetUserById(user.userId);
            userFromDB.isActive = true;
            return userFromDB;
        }

        public IEnumerable<User> GetUsersBySearch(string search, int id) => GetUsersExceptSelf(id).Where(u => u.username.Contains(search));

        public string CheckIfRoomExist(User[] users)
        {
            var usersFromDb = userRepository.GetAllUsers().Where(u => u.userId == users.First().userId || u.userId == users.Last().userId);
            var roomId1 = $"{usersFromDb.First().username}-{usersFromDb.Last().username}";
            var roomId2 = $"{usersFromDb.Last().username}-{usersFromDb.First().username}";

            if (usersFromDb.First().roomId.Contains(roomId1))
                return roomId1;
            else if (usersFromDb.First().roomId.Contains(roomId2))
                return roomId2;

            return "";
        }

        public string CreateRoom(User[] users)
        {
            var usersFromDb = userRepository.GetAllUsers().Where(u => u.userId == users.First().userId || u.userId == users.Last().userId);
            var roomId = $"{usersFromDb.First().username}-{usersFromDb.Last().username}";
            foreach (var u in usersFromDb)
            {
                u.roomId.Add(roomId);
            }
            return roomId;
        }
    }
}
