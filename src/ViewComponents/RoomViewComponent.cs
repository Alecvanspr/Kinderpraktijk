using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;

public class RoomViewComponent : ViewComponent{
/*
    private MijnContext _Context;
    public RoomViewComponent(MijnContext context){
        _Context = context;
    }
    public IViewComponentResult Invoke(){
        var CurrentUserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value; 
        var chats = _Context.ChatUsers
        .Include(x=>x.chat)
        .Where(x=>x.UserId== CurrentUserId)
        .Select(x=>x.chat)
        .ToList();
        return View(chats);
    }
    */
}