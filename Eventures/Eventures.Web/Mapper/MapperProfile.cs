namespace Eventures.Web.Mapper
{
    using System.Globalization;
    using AutoMapper;
    using Eventures.Models;
    using ViewModels.Account;
    using ViewModels.Events;

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            this.CreateMap<User, RegisterViewModel>()
                 .ForMember(u => u.Username, r => r.MapFrom(m => m.UserName))
                 .ReverseMap();

            this.CreateMap<Event, EventViewModel>()
                .ForMember(evm => evm.Start,
                    e => e.MapFrom(s => s.Start.ToString("dd-MMM-yy hh:mm:ss tt", CultureInfo.InvariantCulture)))
                .ForMember(evm => evm.End,
                    e => e.MapFrom(s => s.End.ToString("dd-MMM-yy hh:mm:ss tt", CultureInfo.InvariantCulture)));
        }
    }
}