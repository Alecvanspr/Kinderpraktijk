using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
    public IActionResult Index(){
        ViewData["IsModerator"]=User.IsInRole("Moderator")||User.IsInRole("Pedagoog");
        //met deze method haal het Id van de current User op
        var CurrentUser =User.FindFirst(ClaimTypes.NameIdentifier).Value;
        return View(_context.ChatUsers.Include(x=>x.chat).Where(x=>x.UserId==CurrentUser).Select(x=>x.chat).ToList());
    }
    //Dit is voor het verkrijgen van de view voor het toevoegen van de model
    [HttpGet]
    public IActionResult GroepToevoegen(){
        //hier wordt dan een query uitgehaald
        var CurrentUser =User.FindFirst(ClaimTypes.NameIdentifier).Value;
        return View( _context.Chat.ToList());
    }
    //Deze is voor de chat zelf. Hiermee kan je alle berichten zien
    [HttpGet]
    public IActionResult Chat(int ChatId){
        if(UserIsIn(ChatId))
            return View(_context.Chat.Include(x=>x.Messages).Where(x=>x.Id==ChatId).Single());
        return RedirectToAction("Index",_context.Chat);
    }
    //de berichten hieronder zijn voor het gebruik van de chat
    //dan kan je denken aan het verzenden van berichten en het maken van chats
    [HttpPost]
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
    public IActionResult MaakZelfhulpgroep(){
        if(User.IsInRole("Moderator")||User.IsInRole("Pedagoog"))
            return View();
        else
            return RedirectToAction("AccessDenied","Home");
    }

    //alles van hieronder is nog niet in gebruik genomen
    [HttpPost]
    public async Task<IActionResult> CreateRoom(string naam){
        var chat =new Chat{
            Naam = naam,
            type = ChatType.Room
        };
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
    }

    [HttpPost]
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
    //Door onderstaande kan je zien of een user in een bepaalde class is
    public bool UserIsIn(int ChatId){
        var CurrentUser =User.FindFirst(ClaimTypes.NameIdentifier).Value;
        return _context.ChatUsers.Where(x=>x.ChatId==ChatId).Any(x=>x.UserId==CurrentUser);
    }
}