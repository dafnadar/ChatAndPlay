using ChatWebApi.Models;

namespace ChatWebApi.Repositories
{
    public class MsgRepository : IMsgRepository
    {
        IDictionary<int, Message> MessagesDB = new Dictionary<int, Message>();       

        public void AddMessage(Message msg)
        {
            msg.id = MessagesDB.Count() + 1;
            MessagesDB.TryAdd(msg.id, msg);
        }

        public IEnumerable<Message> LoadMessagesForRoom(string roomId) => MessagesDB.Values.Where(m => m.room == roomId).ToList();
    }
}
