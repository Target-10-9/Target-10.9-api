using AutoMapper;
using Target10._9.Business.ModeDetails.Entities;
using Target10._9.Business.SessionModes.Entities;
using Target10._9.Business.SessionModeWeaponDetails.Entities;
using Target10._9.Business.Sessions.Entities;
using Target10._9.Business.Sessions.Responses;
using Target10._9.Business.WeaponDetails.Entities;

namespace Target10._9.Business.Sessions.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Session, GetSessionsResponse>();
            CreateMap<Session, GetSessionByIdResponse>();

            CreateMap<Session, AddSessionResponse>();
            CreateMap<Session, UpdateSessionByIdResponse>();
            
            CreateMap<Session, GetSessionByIdResponse>();
            
            
            CreateMap<SessionMode, SessionModeDto>()
                .ForMember(dest => dest.SessionModeWeaponDetails, opt => opt.MapFrom(src => src.SessionModeWeaponDetails));
            CreateMap<ModeDetail, ModeDetailDto>();
            CreateMap<SessionModeWeaponDetail, SessionModeWeaponDetailDto>();
            CreateMap<WeaponDetail, WeaponDetailDto>();
        }
    }
}