using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class DashboardController : Controller{
    private MijnContext _context;
    public DashboardController(MijnContext context){
        //Deze moet achteraf ook aangepast worden door de database die we van plan zijn
        //te gebruiken
        _context = context;
    }
    public IActionResult Index(){
        if(User.IsInRole("Ouder"))
        {
            return RedirectToAction("Overzicht");
        }
        //Hier wordt meegegeven of de user een moderator is.
        //op basis hiervan wordt bepaald of de user te zien krijgt of hij een groep aan mag maken of dat hij kan chatten met de pedagoog
        ViewData["IsModerator"] = User.IsInRole("Moderator")||User.IsInRole("Pedagoog");
        //met deze method haal het Id van de current User op
        var CurrentUser =User.FindFirst(ClaimTypes.NameIdentifier).Value;
        return View(_context.ChatUsers.Include(x=>x.chat).Where(x=>x.UserId==CurrentUser).Select(x=>x.chat).ToList());
    }
    //Dit is voor het verkrijgen van de view voor het toevoegen van de model
    [HttpGet]
    [Authorize(Roles = "Moderator,Pedagoog,Client")]

    public IActionResult GroepToevoegen(){
        //Hieronder wordt een lijst van alle chats naarvoren gehaald die publiek zijn
        var GroepenLijst = _context.Chat.Where(x=>x.type==ChatType.Room);
        //Hier wordt die lijst terug geven aan de mensen
        return View(GroepenLijst.ToList());
    }
    //Deze is voor de chat zelf. Hiermee kan je alle berichten zien
    [HttpGet]
    [Authorize(Roles = "Moderator,Pedagoog,Client")]

    public IActionResult Chat(int ChatId){
        if(UserIsIn(ChatId)){
            
            return View(_context.Chat.Include(x=>x.Messages).Where(x=>x.Id==ChatId).Single());
        }
        return RedirectToAction("Index",_context.Chat);
    }
    //de berichten hieronder zijn voor het gebruik van de chat
    //dan kan je denken aan het verzenden van berichten en het maken van chats
    [HttpPost]
    [Authorize(Roles = "Moderator,Pedagoog,Client")]

    public async Task<IActionResult> CreateMessage(int chatId,string message){
        var NewMessage = new Message(){
                ChatId = chatId,
                Text = message,
                Naam = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                timestamp = DateTime.Now
        };

        _context.Messages.Add(NewMessage);
        await _context.SaveChangesAsync();
        return RedirectToAction("Chat",new {id=chatId});
    }
    [Authorize(Roles = "Moderator,Pedagoog")]
    public IActionResult MaakZelfhulpgroep(){
            return View();
    }

    //TODO tests maken voor deze room
    [HttpPost]
    [Authorize(Roles = "Moderator,Pedagoog")]
    public async Task<IActionResult> CreateRoom([Bind("Naam","Beschrijving")]Chat chat){
        if(ModelState.IsValid){
            chat.type = ChatType.Room;
            chat.Users = new List<ChatUser>();
            chat.Users.Add(new ChatUser(){
            //UserId is nu niet van toepassing doordat identity nog niet is geintegreerd in het systeem
            UserId =User.FindFirst(ClaimTypes.NameIdentifier).Value,
            Role = UserRole.Admin,
            ChatId = chat.Id
            });
                
            _context.Chat.Add(chat);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }else{
            Console.WriteLine("modelstate is niet valid");
        }

        return View();
    }
    //Delete room

    //Remove room from list

    //Edit room

    //alles van hieronder is nog niet in gebruik genomen
    [HttpPost]
    [Authorize(Roles = "Moderator,Pedagoog,Client")]
    public async Task<IActionResult> JoinChat(int id){
        if(!UserIsIn(id)){
            var ChatUser = new ChatUser(){
            ChatId = id,
            UserId =User.FindFirst(ClaimTypes.NameIdentifier).Value,
            Role = UserRole.Member,
            };
            _context.ChatUsers.Add(ChatUser);
            await _context.SaveChangesAsync();
            Console.WriteLine("Er is een nieuwe user aan de groep "+ id+ " toegevoegd");
        }
        return RedirectToAction("Chat",new {ChatId = id});
    }
    [Authorize(Roles="Ouder")]
    public ActionResult Overzicht(){
        return View();
    }

    //Door onderstaande kan je zien of een user in een bepaalde class is
    public bool UserIsIn(int ChatId){
        //Hier wordt de current user opgevraagd
        var CurrentUser =User.FindFirst(ClaimTypes.NameIdentifier).Value;
        //hieronder wordt gekeken of de user in de lijst zit van alle users van de aangegeven chat
        return _context.ChatUsers.Where(x=>x.ChatId==ChatId).Any(x=>x.UserId==CurrentUser);
    }
}