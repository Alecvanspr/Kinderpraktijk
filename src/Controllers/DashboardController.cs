using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

//authorize
public class DashboardController : Controller{
    private MijnContext _context;
    public DashboardController(MijnContext context){
        //Deze moet achteraf ook aangepast worden door de database die we van plan zijn
        //te gebruiken
        _context = context;
    }
    public IActionResult Index(){
        return View(_context.Chat.ToList());
    }
    [HttpGet]
    public IActionResult GroepToevoegen(){
        //hier wordt dan een query uitgehaald
        return View(_context.Chat.ToList());
    }
    public IActionResult ChatBody(){
        return View();
    }
    public IActionResult Chat(){
        return View();
    }
    //de berichten hieronder zijn voor het gebruik van de chat
    //dan kan je denken aan het verzenden van berichten en het maken van chats
    [HttpPost]
    public async Task<IActionResult> CreateMessage(int chatId,string message){
        var NewMessage = new Message(){
                ChatId = chatId,
                Text = message,
                Naam = User.Identity.Name,
                timestamp = DateTime.Now
        };

        _context.Messages.Add(NewMessage);
        await _context.SaveChangesAsync();
        return RedirectToAction("Chat",new {id=chatId});
    }

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

    [HttpGet]
    public async Task<IActionResult> JoinChat(int id){
        var ChatUser = new ChatUser(){
            ChatId = id,
            UserId =User.FindFirst(ClaimTypes.NameIdentifier).Value,
            Role = UserRole.Member,
            };
            _context.ChatUsers.Add(ChatUser);
            await _context.SaveChangesAsync();
            return RedirectToAction("Chat","Home",new {id = id});
    }
}