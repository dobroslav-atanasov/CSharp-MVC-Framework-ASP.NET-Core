namespace Eventures.Web.Mapper
{
    using AutoMapper;
    using Eventures.Models;
    using ViewModels.Account;

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
           this.CreateMap<User, RegisterViewModel>()
                .ForMember(u => u.Username, r => r.MapFrom(m => m.UserName))
                .ReverseMap();
        }
    }
}