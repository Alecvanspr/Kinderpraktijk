using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize(Roles = "Moderator")]
public class GroepController:Controller{
    private readonly MijnContext _context;
    public GroepController(MijnContext context){
        _context = context;
    }
    public void CreateGroup(){

    }
    //hiervan de authorization goed doen
    //hiervan ook de classes juist instellen dat alles correct aangeroepen gaat worden
    [Authorize(Roles = "Student")]

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            {
                return NotFound();
            }

            var chat = await _context.Chat
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chat == null)
            {
                return NotFound();
            }

            return View(chat);
    }

}