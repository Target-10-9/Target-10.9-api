using AutoMapper;
using Target10._9.Business.Authentications.Responses;
using Target10._9.Business.Users.Entities;
using Target10._9.Business.Users.Responses;

namespace Target10._9.Business.Users.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, RegisterResponse>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => "Registration successful"));
            CreateMap<User, LoginResponse>();
            
            CreateMap<User, GetUserByIdResponse>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

            CreateMap<User, UpdateUserByIdResponse>();
        }
    }
}