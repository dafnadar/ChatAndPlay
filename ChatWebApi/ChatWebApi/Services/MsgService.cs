using ChatWebApi.Models;
using ChatWebApi.Repositories;

namespace ChatWebApi.Services
{
    public class MsgService : IMsgService
    {

        private IMsgRepository msgRepository;

        public MsgService(IMsgRepository msgRepository) => this.msgRepository = msgRepository;

        public void AddMessageToDB(Message msg) => msgRepository.AddMessage(msg);

        public IEnumerable<Message> LoadMessagesFromDB(string roomId) => msgRepository.LoadMessagesForRoom(roomId);
    }
}
