using AutoMapper;
using src.Areas.Identity.Data;
using src.Areas.Profile.ViewModels;
using System.Collections.Generic;

namespace src.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<srcUser, ProfileViewModel>().ReverseMap();
        }
    }
}
