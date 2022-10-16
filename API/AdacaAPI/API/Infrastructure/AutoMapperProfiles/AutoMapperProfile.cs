using Adaca.Api.DataModels;
using Adaca.Api.DTO;
using Adaca.Api.Models;
using AutoMapper;


namespace Adaca.Api.Infrastructure.AutoMapperProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Client, InsertClientDTO>()
              .ReverseMap();

            CreateMap<ClientListResponse, Client>()
                .ReverseMap()
                .ForMember(p => p.Name, opt => {
                    opt.MapFrom(source=>source.FirstName + "   "+ source.LastName) ;
                });
            ;

        }
    }
}
