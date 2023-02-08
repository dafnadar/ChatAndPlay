using ChatWebApi.Models;

namespace ChatWebApi.Services
{
    public interface IMsgService
    {
        void AddMessageToDB(Message msg);
        IEnumerable<Message> LoadMessagesFromDB(string roomId);
    }
}
