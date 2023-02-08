using ChatWebApi.Models;

namespace ChatWebApi.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();

        User GetUserByUsername(string username);
        User GetUserById(int id);

        void AddNewUser(User user);

    }
}
