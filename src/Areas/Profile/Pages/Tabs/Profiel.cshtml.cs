using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using src.Areas.Profile.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using AutoMapper;
using System.Collections.Generic;
using src.Models;
using System.Threading.Tasks;
using System;

namespace src.Areas.Profile.Pages.Tabs
{
    public class ProfielModel : PageModel
    {
        private readonly MijnContext _context;
        private readonly UserManager<srcUser> _userManager;
        private readonly IMapper _mapper;

        public ProfielModel(MijnContext context, UserManager<srcUser> userManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }

        public List<ProfileViewModel> ProfileViewModel { get; set; }
        //Dit is het profiel van de ingelogde gebruiker
        public ProfileViewModel MijnProfiel { get; set; }   
        //Dit geeft een lijst weer van alle aanmeldingen     
        public List<Aanmelding> Aanmeldingen {get;set;} = new List<Aanmelding>();    
        //Dit geeft set de CurrentUser
        public srcUser CurrentUser{get;set;}

        [BindProperty]
        public bool Aangemeld { get; set; }
        [BindProperty]
        public bool Afgemeld { get; set; }

        public string SpecialistName { get; set; }
        

        public async Task OnGetAsync(bool aan, bool af)
        {
            //Als er een filter functie is deze aanpassen
            UserProfileInfo(true, af);
        }

        public async Task<IActionResult> OnPostMeldAan(string id)
        {
            CurrentUser = await _context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
            CurrentUser.SpecialistId = _userManager.GetUserId(User);

            var query = await _context.Aanmeldingen
                .Where(x => x.ClientId == id)
                .Where(x => x.PedagoogId == _userManager.GetUserId(User))
                .OrderByDescending(x=> x.Id)
                .FirstOrDefaultAsync();

            query.IsAangemeld = true;
            query.AanmeldingDatum = DateTime.UtcNow;

            _context.Aanmeldingen.Update(query);
            _context.Users.Update(CurrentUser);
            _context.SaveChanges();
            return RedirectToPage("/Tabs/Profiel", new { Area = "Profile" });
        }

        public async Task<IActionResult> OnPostMeldAf(string id)
        {
            var query = await _context.Aanmeldingen
                .Where(x => x.ClientId == id && x.PedagoogId == _userManager.GetUserId(User))
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync();

            var clearSpecialistId = await _context.Users
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            clearSpecialistId.SpecialistId = "";

            query.IsAfgemeld = true;
            query.AfmeldingDatum = DateTime.UtcNow;

            _context.Aanmeldingen.Update(query);
            _context.Users.Update(clearSpecialistId);
            _context.SaveChanges();
            return RedirectToPage("/Tabs/Profiel", new { Area = "Profile" });
        }

        public IActionResult OnPostFilter(bool af, bool aan)
        {
            Aangemeld = aan;
            Afgemeld = af;

            return RedirectToPage("/Tabs/Profiel", new { Area = "Profile", aan = Aangemeld, af = Afgemeld});
        }
    
        public async void UserProfileInfo(bool aan, bool af)
        {
            //Dit is de Current User
            var currentUserId = _userManager.GetUserId(User);
            CurrentUser = _context.Users
                                        .Include(x=>x.Childeren)
                                        .Where(x=>x.Id== currentUserId).SingleOrDefault();

            //HIermee wordt een lijst van alle kinderen van de user opgevraagd
            var child = await _context.Users
                                        .Where(x => x.ParentId == currentUserId)
                                        .ToListAsync();

            //Dit is het specialist id
            var specialist = _context.Users
                            .Include(x=>x.Specialist)
                            .Select(x=>x.Specialist);
            //Hiermee wordt een lijst met de actieve aanmeldingen gegenereerd
            Aanmeldingen = GetAanmeldingen(currentUserId,aan,af);

            ProfileViewModel = _mapper.Map<List<srcUser>, List<ProfileViewModel>>(child);
            MijnProfiel = _mapper.Map<srcUser, ProfileViewModel>(CurrentUser);      
        }
        //Hiermee wordt de actieve lijst gehaald met alle Aanmeldingen
        public List<Aanmelding> GetAanmeldingen(string currentUserId,bool aan, bool af){
                return _context.Aanmeldingen
                                            .Include(x=>x.Client)
                                            .Include(x=>x.Pedagoog)
                                            .Where(x=>x.PedagoogId==currentUserId)
                                            .ToList();
        }
    }
}
