using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

public class ChatHub : Hub 
{
    //Deze methode send een message
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
        public Task JoinRoom(string roomId)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, roomId);
        }

        public Task LeaveRoom(string roomId)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);
        }
}