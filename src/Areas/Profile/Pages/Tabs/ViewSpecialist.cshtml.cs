using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using src.Areas.Identity.Data;
using src.Areas.Profile.ViewModels;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using AutoMapper;
using System.Collections.Generic;
using System;

namespace src.Areas.Profile.Pages.Tabs
{
    public class ViewSpecialistModel : PageModel
    {

        private readonly MijnContext _context;
        private readonly UserManager<srcUser> _userManager;
        private readonly IMapper _mapper;

        public ViewSpecialistModel(MijnContext context, UserManager<srcUser> userManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }

        public List<ProfileViewModel> ProfileViewModel { get; set; }

        public void OnGet()
        {

            var result = (from s in _context.Users
                          where !String.IsNullOrEmpty(s.Specialism)
                          select s).ToList();

            ProfileViewModel = _mapper.Map<List<srcUser>, List<ProfileViewModel>>(result);
        }
    }
}