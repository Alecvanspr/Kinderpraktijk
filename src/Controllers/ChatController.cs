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
    //dit is voor het maken van de groups
    //Als er een bericht wordt verstuurd. Dan wordt zo'n bericht async verstuurd
    [HttpPost("[action]/{connectionId}/{RoomName}")]
    public async Task<IActionResult> joinRoom(string connectionId, string RoomName){
            //hiermee wordt een user aan een room group gekoppeld inplaats van dat alle berichten bij iederen komen
            Console.WriteLine("De user is aan de groep "+ RoomName+ " toegevoegd");
            await _chat.Groups.AddToGroupAsync(connectionId, RoomName);
            return Ok();
    }
    [HttpPost("[action]/{connectionId}/{RoomName}")]
        public async Task<IActionResult> LeaveGroup(string connectionId, string RoomName){
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
            //await _chat.Clients.All.SendAsync("ReceiveMessage",NewMessage);

           await _chat.Clients.Group(chatId+"").SendAsync("ReceiveMessage", NewMessage);
            //Dit gaat een bericht sturen naar de client
            return Ok();
    }
}