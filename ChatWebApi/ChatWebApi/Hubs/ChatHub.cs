using Microsoft.AspNetCore.SignalR;

namespace ChatWebApi.Hubs
{
    public class ChatHub : Hub
    {
        public Task SendMessageToGroup(string room, string sender, string text) => Clients.Group(room).SendAsync("ReceiveMessage", sender, text);

        public Task JoinRoom(string room) => Groups.AddToGroupAsync(Context.ConnectionId, room);
    }
}
