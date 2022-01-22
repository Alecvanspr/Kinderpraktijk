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

        public List<ProfileViewModel> ProfileViewModel { get; set; } = new List<ProfileViewModel>();
        //Dit is het profiel van de ingelogde gebruiker
        public ProfileViewModel MijnProfiel { get; set; }
        //Dit geeft een lijst weer van alle aanmeldingen     
        public List<Aanmelding> Aanmeldingen {get;set;} = new List<Aanmelding>();    
        //Dit geeft set de CurrentUser
        public srcUser CurrentUser{get;set;}

        //Deze worden automatisch geset
        //Door deze filter zie je al gelijk de dingen die geactiveerd zijn
        [BindProperty]
        public bool Aangemeld { get; set; } = true;
        [BindProperty]
        public bool Afgemeld { get; set; } = false;

        public string SpecialistName { get; set; }
        

        public async Task OnGetAsync(bool aan, bool af)
        {
            //Als er een filter functie is deze aanpassen
            var currentUserId = _userManager.GetUserId(User);
            SetCurrentUser(currentUserId);
            //Hiermee wordt een lijst met de actieve aanmeldingen gegenereerd
            //Daarvoor moet je de speciale FilterList aanroepe
            Aanmeldingen = GetFilters(GetAanmeldingen(currentUserId)).ToList();
        }

        public async Task<IActionResult> OnPostMeldAan(string id)
        {
            CurrentUser = await _context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
            CurrentUser.SpecialistId = _userManager.GetUserId(User);
            //hier wordt de laatste aanmelding die is gemaakt geopend
            var LaasteAanmelding = GetAanmeldingen(CurrentUser.SpecialistId)
                                                     .First();
            
            //Hier wordt de laatste aanmelding op waar gezet
            LaasteAanmelding.IsAangemeld = true;
            LaasteAanmelding.AfmeldingDatum =  DateTime.Now;

            //Hier wordt de aanmelding die gedaan wordt opgeslagen
            _context.Aanmeldingen.Update(LaasteAanmelding);
            _context.Users.Update(CurrentUser);
            _context.SaveChanges();
            return RedirectToPage("/Tabs/Profiel", new { Area = "Profile" });
        }

        public async Task<IActionResult> OnPostMeldAf(string id)
        {
            CurrentUser = await _context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();

            var LaasteAanmelding = GetFilters(GetAanmeldingen(CurrentUser.SpecialistId))
                                                     .First();
            //Hier wordt de Specialist van de Current User verwiderd
            CurrentUser.SpecialistId = null;

            //hier wordt de afmelding waar gemaakt
            LaasteAanmelding.IsAfgemeld = true;
            LaasteAanmelding.AfmeldingDatum = DateTime.Now;

            _context.Aanmeldingen.Update(LaasteAanmelding);
            _context.Users.Update(CurrentUser);
            _context.SaveChanges();
            return RedirectToPage("/Tabs/Profiel", new { Area = "Profile" });
        }

        public IActionResult OnPostFilter(bool aan, bool af)
        {
            Aangemeld = aan;
            Afgemeld = af;

            return RedirectToPage("/Tabs/Profiel", new { Area = "Profile", aan = Aangemeld, af = Afgemeld});
        }
        public void SetCurrentUser(string currentUserId){
            CurrentUser = _context.Users
                                        .Include(x=>x.Childeren)
                                        .Where(x=>x.Id== currentUserId).SingleOrDefault();
        }
        //Hier wordt een lijst met kinderen opgevraagd
        public async Task<List<srcUser>> GetChildAsync(string currentUserId){ 
           return await _context.Users
                                        .Where(x => x.ParentId == currentUserId)
                                        .ToListAsync();
        }
        //Hier kan je filters toevoegen van aan de Aanmeldingen lijst
        public IQueryable<Aanmelding> GetFilters(IQueryable<Aanmelding> lijst){
                return lijst.Where(x=>x.IsAangemeld==Aangemeld).Where(x=>x.IsAfgemeld==Afgemeld);
        }
        //Hier wordt een lijst van alle aanmeldingen gegeven
        public IQueryable<Aanmelding> GetAanmeldingen(string PedagoogId){
                return _context.Aanmeldingen
                                            .Include(x=>x.Client)
                                            .Include(x=>x.Pedagoog)
                                            .Where(x=>x.PedagoogId==PedagoogId);
        }
    }
}
