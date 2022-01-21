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
        public ProfileViewModel MijnProfiel { get; set; }
        public List<AanmeldingClient> Aanmeldingen { get; set; } = new List<AanmeldingClient>();
        public List<srcUser> Clienten { get; set; } = new List<srcUser>();
        public List<ClientListAanmelding> ClientList { get; set; } = new List<ClientListAanmelding>();
        
        public List<ClientRelations> ClientRelations { get; set; } = new List<ClientRelations>();

        [BindProperty]
        public bool Aangemeld { get; set; }
        [BindProperty]
        public bool Afgemeld { get; set; }

        public string SpecialistName { get; set; }

        public void OnGet(bool aan, bool af)
        {
            UserProfileInfo();
            SpecialistRelations();
            ClientenRelationsList(aan, af);          
        }

        public async Task<IActionResult> OnPostMeldAan(string id)
        {
            var user = await _context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
            user.SpecialistId = _userManager.GetUserId(User);

            var query = await _context.Aanmeldingen
                .Where(x => x.ClientId == id)
                .Where(x => x.srcUserId == _userManager.GetUserId(User))
                .OrderByDescending(x=> x.Id)
                .FirstOrDefaultAsync();

            query.IsAangemeld = true;
            query.Aanmelding = DateTime.UtcNow;

            _context.Aanmeldingen.Update(query);
            _context.Users.Update(user);
            _context.SaveChanges();
            return RedirectToPage("/Tabs/Profiel", new { Area = "Profile" });
        }

        public async Task<IActionResult> OnPostMeldAf(string id)
        {
            var query = await _context.Aanmeldingen
                .Where(x => x.ClientId == id && x.srcUserId == _userManager.GetUserId(User))
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync();

            var clearSpecialistId = await _context.Users
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            clearSpecialistId.SpecialistId = "";

            query.IsAfgemeld = true;
            query.Afmelding = DateTime.UtcNow;

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

        public void ClientenRelationsList(bool aan, bool af)
        {
            ClientRelations = (from a in _context.Users
                               join b in _context.Aanmeldingen on a.Id equals b.ClientId
                               join c in _context.Users on b.srcUserId equals c.Id
                               //where b.IsAangemeld == aan || b.IsAfgemeld == af
                               select new ClientRelations
                               {
                                   ClientName = a.FirstName + " " + a.LastName,
                                   SpecialistName = c.FirstName + " " + c.LastName,
                                   DateAanmelding = b.Aanmelding,
                                   DateAfmelding = b.Afmelding,
                                   IsAangemeld = b.IsAangemeld,
                                   IsAfgemeld = b.IsAfgemeld
                               }).ToList();

            foreach (var x in ClientRelations.ToList())
            {
                if (x.IsAangemeld == aan || x.IsAfgemeld == af)
                {
                    ClientRelations.Remove(x);
                }
            }
        }

        public async void UserProfileInfo()
        {
            var user = await (from s in _context.Users
                                where s.Id == _userManager.GetUserId(User)
                                select s).SingleAsync();

            var child = await (from s in _context.Users
                               where s.ParentId == _userManager.GetUserId(User)
                               select s).ToListAsync();

            var specialist = (from l in _context.Users
                             where l.Id == _userManager.GetUserId(User)
                             select l.SpecialistId).FirstOrDefault();

            Aanmeldingen = await (from l in _context.Aanmeldingen
                                  where l.srcUserId == _userManager.GetUserId(User)
                                  select l).ToListAsync();

            Clienten = await (from l in _context.Users
                              where l.Id == Aanmeldingen.Select(x => x.ClientId).DefaultIfEmpty("").First().ToString()
                              select l).ToListAsync();

            SpecialistName = (from l in _context.Users
                              where l.Id == specialist
                              select l.FirstName + " " + l.LastName).FirstOrDefault();

            ProfileViewModel = _mapper.Map<List<srcUser>, List<ProfileViewModel>>(child);
            MijnProfiel = _mapper.Map<srcUser, ProfileViewModel>(user);      
        }

        public void SpecialistRelations()
        {
            ClientList = Aanmeldingen.Join(
                Clienten,
                client => client.ClientId,
                user => user.Id,
                (client, user) => new ClientListAanmelding
                {
                    ClientName = user.FirstName + " " + user.LastName,
                    ClientId = client.ClientId,
                    DateAanmelding = client.Aanmelding,
                    DateAfmelding = client.Afmelding,
                    IsAangemeld = client.IsAangemeld,
                    IsAfgemeld = client.IsAfgemeld
                }).ToList();
        }
    }
}
