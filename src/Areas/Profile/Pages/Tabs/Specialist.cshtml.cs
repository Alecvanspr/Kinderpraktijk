using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        public srcUser pedagoog;
        public void OnGet()
        {
            var UserId =User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = _context.Users.Where(x=>x.Id==UserId).SingleOrDefault();
            ViewData["IsPedagoog"]=false;
            if(User.IsInRole("Pedagoog")){
                ViewData["IsPedagoog"]=true;
                ViewData["HeeftPedagoog"]=false;
                Console.WriteLine("De user is een pedagoog");
            }else{
            ViewData["IsPedagoog"]=false;
            var Specialist = _context.Users.Where(x=>x.Id==user.SpecialistId).SingleOrDefault();
            if(Specialist==null){
                ViewData["HeeftPedagoog"]=false;
                Console.WriteLine("Geen pedagoog");
            }else{
                ViewData["HeeftPedagoog"]=true;
                Console.WriteLine("User heeft pedagoog");
                pedagoog = Specialist;
                }
            }
        }
    }
}
