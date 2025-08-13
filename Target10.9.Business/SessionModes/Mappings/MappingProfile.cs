using AutoMapper;
using Target10._9.Business.SessionModes.Entities;
using Target10._9.Business.SessionModes.Responses;
using Target10._9.Business.SessionModeWeaponDetails.Entities;
using Target10._9.Business.SessionModeWeaponDetails.Responses;
using Target10._9.Business.WeaponDetails.Entities;

namespace Target10._9.Business.SessionModes.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SessionMode, GetSessionModesResponse>();
            CreateMap<SessionMode, GetSessionModeByIdResponse>();
            CreateMap<WeaponDetail, GetAuthorizedWeaponResponse>();
            
            CreateMap<SessionMode, AddSessionModeResponse>();
            CreateMap<SessionModeWeaponDetail, AddAuthorizedWeaponResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.WeaponDetails.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.WeaponDetails.Name))
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.WeaponDetails.Brand))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.WeaponDetails.Description))
                .ForMember(dest => dest.SerialNumber, opt => opt.MapFrom(src => src.WeaponDetails.SerialNumber))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.WeaponDetails.UserId));

            CreateMap<SessionMode, UpdateSessionModeByIdResponse>();
        }
    }
}