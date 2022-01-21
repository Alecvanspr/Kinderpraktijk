using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using src.Areas.Profile.ViewModels;

namespace src.Areas.Profile.Pages.Tabs
{
    public class ClientenOverzichtModel : PageModel
    {
        private readonly MijnContext _context;
        private readonly UserManager<srcUser> _userManager;
        private readonly IMapper _mapper;

        public ClientenOverzichtModel(MijnContext context, UserManager<srcUser> userManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }

        public List<srcUser> users;

        public void OnGet(string sorteer)
        {
            string specialistId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            users = _context.Users.Where(p => p.SpecialistId == specialistId).ToList();
        }

        public async Task<IActionResult> OnPost(string id)
        {
            srcUser user = await _context.Users.Where(p => p.Id == id).FirstOrDefaultAsync();
            if(user.UserBlocked)
            {
                user.UserBlocked = false;
                await _context.SaveChangesAsync();
            } else {
                user.UserBlocked = true;
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("/Tabs/ClientenOverzicht");
        }

        // public IQueryable<srcUser> Sorteer(IQueryable<srcUser> lijst, string sorteer)
        // {
        //     if(sorteer == "voornaam_oplopend")
        //     {
        //         return lijst.OrderBy(p => p.FirstName.ToLower());
        //     } else if(sorteer == "voornaam_aflopend")
        //     {
        //         return lijst.OrderByDescending(p => p.FirstName.ToLower());
        //     } else if(sorteer == "achternaam_oplopend")
        //     {
        //         return lijst.OrderBy(p => p.LastName.ToLower());
        //     } else if(sorteer == "achternaam_aflopend")
        //     {
        //         return lijst.OrderByDescending(p => p.LastName.ToLower());
        //     } else if(sorteer == "email_oplopend")
        //     {
        //         return lijst.OrderBy(p => p.Email.ToLower());
        //     } else
        //     {
        //         return lijst.OrderByDescending(p => p.Email.ToLower());
        //     }
        // }
    }
}