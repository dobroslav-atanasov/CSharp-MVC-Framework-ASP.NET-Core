using AutoMapper;
using Eventures.Models;
using Eventures.Web.ViewModels.Account;

namespace Eventures.Web.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, RegisterViewModel>()
                .ForMember(u => u.Username, r => r.MapFrom(m => m.UserName))
                .ReverseMap();
        }
    }
}
