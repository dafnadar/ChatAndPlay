using ChatWebApi.Models;

namespace ChatWebApi.Services
{
    public interface IUserService
    {
        string IsValidUser(User user);
        string IsNewUser(User user);
        string Login(User user);

        void AddNewUser(User user);
        User GetUserByUsername(string username);
        User GetUserById(int id);
        User SetActiveUser(User user);
        IEnumerable<User> GetUsersExceptSelf(int id);
        IEnumerable<User> GetUsersBySearch(string search, int id);
        string CheckIfRoomExist(User[] users);
        string CreateRoom(User[] users);

        void Logout(int id);
    }
}
