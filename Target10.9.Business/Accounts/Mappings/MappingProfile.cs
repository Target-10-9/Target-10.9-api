using AutoMapper;
using Target10._9.Business.Accounts.Entities;
using Target10._9.Business.Accounts.Responses;

namespace Target10._9.Business.Accounts.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, RegisterResponse>()
                .ForMember(dest => dest.FullName, 
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.Message, 
                    opt => opt.MapFrom(_ => "Inscription réussie."));
        }
    }
}