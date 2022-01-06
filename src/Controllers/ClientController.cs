using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize(Roles = "Pedagoog , Moderator")]
public class ClientController: Controller{
    private MijnContext _context;
  private srcUser _currentUser;
 public srcUser currentUser 
 {
    get { 
        if(_currentUser==null){
            var CurrentUserId=User.FindFirst(ClaimTypes.NameIdentifier).Value;
            _currentUser =_context.Users.Where(x=>x.Id==CurrentUserId).Single();
        }
        return _currentUser;
        }
    set { _currentUser = value;}
 }
    
    public ClientController(MijnContext context){
        _context =context;
    }
    //TODO De user moet automatisch gelinkt worden met de pedagoog bij het aanmelden.
    //Bij het aanmelden van de user en de pedagoog moet deze groep automatisch aangemaakt 

    //TODO Als er een link is tussen de Users en de Pedagoogen daar ook een list van laten weergeven
    public ActionResult Index(){
        //hiermee worden alle privé chats toegevoegd
        var CurrentUser =User.FindFirst(ClaimTypes.NameIdentifier).Value;

        //Hier in de list wordt gekeken of de users in de chat zitten.
        //dan wordt gekeken of de chat een private chat is
        var PrivateLists = _context.ChatUsers.Include(x=>x.chat)
                                                                        .Where(x=>x.UserId==CurrentUser)
                                                                        .Select(x=>x.chat)
                                                                        .Where(x=>x.type==ChatType.Private);
        return View(PrivateLists.ToList());
    }
  //TODO tests maken voor deze room
    [HttpPost]
    [Authorize(Roles = "Moderator,Pedagoog")]
    public async Task<IActionResult> CreateRoom([Bind("Naam","Beschrijving")]Chat chat){
        if(ModelState.IsValid){
            chat.type = ChatType.Private;
            chat.Users = new List<ChatUser>();
            chat.Users.Add(new ChatUser(){
                UserId =User.FindFirst(ClaimTypes.NameIdentifier).Value,
                Role = UserRole.Admin,
                ChatId = chat.Id
            });  
            //Hieronder moet een een user aangemaakt worden voor degene waarvoor de chat is
            _context.Chat.Add(chat);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }else{
            Console.WriteLine("modelstate is niet valid");
        }
        return View();
    }
}