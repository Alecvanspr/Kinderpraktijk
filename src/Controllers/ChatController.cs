using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

public class ChatController : Controller{
    private IHubContext<ChatHub> _chat;
    public ChatController( IHubContext<ChatHub> chat){
            _chat = chat;
    }
    [HttpPost("[action]/{connectionId}/{RoomName}")]
    public async Task<IActionResult> JoinRoomAsync(string connectionId, string RoomName){
            await _chat.Groups.AddToGroupAsync(connectionId, RoomName);
            return Ok();
    }
    [HttpPost("[action]/{connectionId}/{RoomName}")]
        public async Task<IActionResult> LeaveRoom(string connectionId, string RoomName){
            await _chat.Groups.RemoveFromGroupAsync(connectionId, RoomName);
            return Ok();
    }
    //deze methode vindt plaats bij die client
    public async Task<IActionResult> SendMessage(
        int chatId,
        string message,
        string roomName,
        [FromServices] MijnContext _context
        ){
       var NewMessage = new Message(){
                    ChatId = chatId,
                    Text = message,
                    Naam = User.Identity.Name,
                    timestamp = DateTime.Now
            };
            //Naam is NULL doordat de identity nog niet goed is ingesteld
            _context.Messages.Add(NewMessage);
            await _context.SaveChangesAsync();

            //dit doet het niet ofzo
            //Deze wordt wel verzonden, echter gaat dit naar alle clients. Ik den dat de GROUP niet werkt van de andere
            await _chat.Clients.All.SendAsync("ReceiveMessage",NewMessage);
            //hieronder wordt het naar alle gebruikers van die group gestuurd
            //Console.WriteLine(roomName);
            
            //await _chat.Clients.Groups(roomName).SendAsync("RecieveMessage",NewMessage);
            //Dit gaat een bericht sturen naar de client
            return Ok();
    }
}