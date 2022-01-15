using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace src.Areas.Profile.Pages.Tabs
{
    public class SpecialistModel : PageModel
    {
        private readonly MijnContext _context;
        private readonly IMapper _mapper;
        public SpecialistModel(MijnContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public bool heeftPedagoog;
        public bool IsPedagoog;
        public static bool successvolToegevoegd;
        public static bool nietSuccesvolToegevoegd;
        public bool getSuccessvol(){
            var ret= successvolToegevoegd;
            successvolToegevoegd = false;
            return ret;
        }
        public bool getNietSuccessvol(){
            var ret = nietSuccesvolToegevoegd;
            nietSuccesvolToegevoegd =false;
            return ret;
        }

        public srcUser pedagoog;
        //Onderstaande methode wordt opgeroepen bij het laden van de pagina
        //In de methode wordt er gekeken of de huidige user een pedagoog heeft
        public IActionResult OnGet()
        {
            var user = getUser();
            heeftPedagoog=false;
            IsPedagoog = false;
            if(User.IsInRole("Pedagoog")){ //Hier is de user een pedagoog
                IsPedagoog=true;
                pedagoog = user;
            }else{
            var Specialist = _context.Users.Where(x=>x.Id==user.SpecialistId).SingleOrDefault();
            if(Specialist==null){//Heeft geen pedagoog
                heeftPedagoog=false;
            }else{//Heeft een pedagoog
                heeftPedagoog=true;
                pedagoog = Specialist;
                }
            }
            return Page();
        }
        [HttpPost]
        public async Task<IActionResult> OnPostConnectWithPedagoog(string Id){
            //Vind pedagoog
            var pedagoog = _context.Users.Where(x=>x.Id==Id).SingleOrDefault();
            if(pedagoog!=null){
                var user = getUser();
                user.SpecialistId = pedagoog.Id;
                await CreateNewGroupAsync(user,pedagoog);
                _context.SaveChanges();
                successvolToegevoegd = true;
            }else{
                //hier moet een foutmelding geven dat de pedagoog niet gevonden is
                nietSuccesvolToegevoegd = true;
            }
            return RedirectToPage("/Tabs/Specialist", new { Area = "Profile" });
        }
        public srcUser getUser(){
                var UserId =User.FindFirst(ClaimTypes.NameIdentifier).Value;
                return _context.Users.Where(x=>x.Id==UserId).SingleOrDefault();
        }
        [HttpPost]
        public async Task<bool> CreateNewGroupAsync(srcUser user, srcUser pedagoog){
            Chat chat = new Chat(){Naam="Prive chat "+user.LastName,Beschrijving="Dit is de prive chat tussen jou en de pedagoog", type=ChatType.Private};
            chat.Users = new List<ChatUser>(){
                new ChatUser(){UserId= user.Id, Role=UserRole.Member,ChatId = chat.Id},
                new ChatUser(){UserId=pedagoog.Id, Role=UserRole.Admin, ChatId = chat.Id}
            };
            _context.Chat.Add(chat);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
