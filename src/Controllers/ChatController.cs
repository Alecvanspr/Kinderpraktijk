using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

[Authorize]
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
        var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var currentUser = _context.Users.Where(x=>x.Id==currentUserId).First();
        var Username = currentUser.Firstname+" "+currentUser.LastName;
       var NewMessage = new Message(){
                    ChatId = chatId,
                    Text = message,
                    Naam = Username,
                    timestamp = DateTime.Now
            };

            //Naam is NULL doordat de identity nog niet goed is ingesteld
            _context.Messages.Add(NewMessage);
            await _context.SaveChangesAsync();

            //Deze wordt wel verzonden, echter gaat dit naar alle clients. Ik den dat de GROUP niet werkt van de andere
            await _chat.Clients.All.SendAsync("ReceiveMessage",NewMessage);
            
            //hieronder wordt het naar alle gebruikers van die group gestuurd
            //Console.WriteLine(roomName);
            //Voor de groups heb ik identity nodig
            //await _chat.Clients.Groups(roomName).SendAsync("RecieveMessage",NewMessage);
            //Dit gaat een bericht sturen naar de client
            return Ok();
    }
}