using ChatWebApi.Models;

namespace ChatWebApi.Repositories
{
    public interface IMsgRepository
    {
        void AddMessage(Message msg);
        IEnumerable<Message> LoadMessagesForRoom(string roomId);
    }
}
