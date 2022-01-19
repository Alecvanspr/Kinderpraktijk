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
        public string SpecialistName { get; set; }

        public async void OnGetAsync()
        {
            var parent = await (from s in _context.Users
                          where s.Id == _userManager.GetUserId(User)
                          select s).SingleAsync();


            var child = await (from s in _context.Users
                         where s.ParentId == _userManager.GetUserId(User)
                         select s).ToListAsync();

            Aanmeldingen = await (from l in _context.AanmeldingenClients
                            where l.srcUserId == _userManager.GetUserId(User)
                            select l).ToListAsync();

            Clienten =  await (from l in _context.Users
                        where l.Id == Aanmeldingen.Select(x=>x.ClientId).DefaultIfEmpty("").First().ToString()
                        select l).ToListAsync();

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


            ProfileViewModel = _mapper.Map<List<srcUser>, List<ProfileViewModel>>(child);
            MijnProfiel = _mapper.Map<srcUser, ProfileViewModel>(parent);

            var myProfile = (from l in _context.Users
                                 where l.Id == _userManager.GetUserId(User)
                                 select l.SpecialistId).FirstOrDefault();

            SpecialistName = (from l in _context.Users
                                 where l.Id == myProfile
                                 select l.FirstName + " " + l.LastName).FirstOrDefault();
        }

        public async Task<IActionResult> OnPostMeldAan(string id)
        {
            var user = await _context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
            user.SpecialistId = _userManager.GetUserId(User);

            var query = await _context.AanmeldingenClients
                .Where(x => x.ClientId == id)
                .Where(x => x.srcUserId == _userManager.GetUserId(User))
                .OrderByDescending(x=> x.Id)
                .FirstOrDefaultAsync();

            query.IsAangemeld = true;

            _context.AanmeldingenClients.Update(query);
            _context.Users.Update(user);
            _context.SaveChanges();
            return RedirectToPage("/Tabs/Profiel", new { Area = "Profile" });
        }

        public async Task<IActionResult> OnPostMeldAf(string id)
        {
            var query = await _context.AanmeldingenClients
                .Where(x => x.ClientId == id && x.srcUserId == _userManager.GetUserId(User))
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync();

            query.IsAfgemeld = true;

            _context.AanmeldingenClients.Update(query);
            _context.SaveChanges();
            return RedirectToPage("/Tabs/Profiel", new { Area = "Profile" });
        }
    }
}
