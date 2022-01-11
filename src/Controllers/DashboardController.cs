using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize]
public class DashboardController : Controller{
    private MijnContext _context;
    public DashboardController(MijnContext context){
        //Deze moet achteraf ook aangepast worden door de database die we van plan zijn
        //te gebruiken
        _context = context;
    }
    //Getest
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
            ViewData["IsModerator"] = User.IsInRole("Moderator")||User.IsInRole("Pedagoog");
            return View(_context.Chat.Include(x=>x.Messages).Where(x=>x.Id==ChatId).Single());
        }
        return RedirectToAction("Index",_context.Chat);
    }

    [Authorize(Roles = "Moderator,Pedagoog")]
    public IActionResult MaakZelfhulpgroep(){
            return View();
    }
    //Deze methode is voor het aanmaken van een Chatroom
    [HttpPost]
    [Authorize(Roles = "Moderator,Pedagoog")]
    public async Task<IActionResult> CreateRoom([Bind("Naam","Beschrijving")]Chat chat){
        if(ModelState.IsValid){
            chat.type = ChatType.Room;
            chat.Users = new List<ChatUser>();
            chat.Users.Add(new ChatUser(){
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
    //Deze methode laat de gegevens van de huidige chat zien
    //Dit is een get Als een user hier geen toegang tot heeft wordt hij ook geweigerd
    [HttpGet]
    [Authorize(Roles = "Moderator,Pedagoog,Admin")]
    public IActionResult Details(int chatId){
        //Misschien voor de zekerheid een check hier doen
        if(UserIsIn(chatId)){
            ViewData["Users"] =  _context.Chat.Where(x=>x.Id==chatId).Include(x=>x.Users).Single().Users.Count();
            return View(_context.Chat.Where(x=>x.Id==chatId).Single());
        }
        return RedirectToAction("NotAuthorized");
    }

    //Deze methode is verantwoordelijk voor het verwijderen van een chat uit de chat list
    //Mischien een extra vertificatie toevoegen
    [HttpGet]
    [Authorize(Roles = "Moderator,Pedagoog")]
    public IActionResult DeleteRoom(int Id,bool error){
        if(UserIsIn(Id)){
            ViewData["Gelukt"] = !error;
            return View(_context.Chat.Where(x=>x.Id==Id).Single());
            }
        return RedirectToAction("NotAuthorized");
    }
    [HttpPost]
    [Authorize(Roles = "Moderator,Pedagoog")]
    public IActionResult DeleteRoom(int ChatRoomId,string ChatRoomName){
        if(UserIsIn(ChatRoomId)){
            var chat = _context.Chat.Find(ChatRoomId);
            if(chat.Naam.Equals(ChatRoomName)){
                //Dit is om alle verbindingen die gemaakt zijn met de chat ook gelijk worden verwijderd
                foreach(var item in _context.ChatUsers.Where(x=>x.ChatId==ChatRoomId)){
                        _context.ChatUsers.Remove(item);
                }
                _context.Chat.Remove(chat);
                _context.SaveChanges();
                return RedirectToAction("DeleteRoom",new{Id= chat.Id , error=false});
            }else{
                return RedirectToAction("DeleteRoom",new{Id= chat.Id , error=true});
            }
        }
        return RedirectToAction("NotAuthorized");
    }
    //Remove room from list
    //Als je een owner bent van de 
    [HttpPost]
    [Authorize(Roles = "Moderator,Pedagoog,Client")]
    public IActionResult RemoveRoomFromList(int chatId){
        if(UserIsIn(chatId)){
            var userid =User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var ChatUser = _context.ChatUsers.Where(x=>x.ChatId==chatId).Where(x=>x.UserId==userid).Single();
            _context.ChatUsers.Remove(ChatUser);
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
    }
    //Edit room
    [HttpGet]
    [Authorize(Roles = "Moderator,Pedagoog")]
    public IActionResult Edit(int Id){
        if(UserIsIn(Id)){
            return View(_context.Chat.Where(x=>x.Id==Id).SingleOrDefault());
        }
        return RedirectToAction("NotAuthorized");
    }
    [HttpPost]
    [Authorize(Roles = "Moderator,Pedagoog")]
    public IActionResult Edit(int Id,string naam,string beschrijving){
        if(UserIsIn(Id)){
            var chat = _context.Chat.Where(x=>x.Id==Id).SingleOrDefault();
            chat.Naam = naam;
            chat.Beschrijving = beschrijving;
            _context.Chat.Update(chat);
            _context.SaveChanges();
            return RedirectToAction("Details",Id);
        }
        return RedirectToAction("NotAuthorized");
    }
    
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
    public ActionResult NotAuthorized(){
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

    /*
    //De onderstaande methode wordt niet gebruikt bij het verzenden van een bericht
    [HttpPost]
    [Authorize(Roles = "Moderator,Pedagoog,Client")]
    //Deze methode is echter nog aanvalbaar 
    public async Task<IActionResult> CreateMessage(int chatId,string message){
        //Hierbij wordt gekeken of de user in de chat zit. Als dat niet het geval is dan wordt hij terug gestuurd naar de index pagina
        if(UserIsIn(chatId)){
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
        return RedirectToAction("index");
    }
    */