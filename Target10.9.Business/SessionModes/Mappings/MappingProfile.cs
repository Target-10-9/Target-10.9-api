using AutoMapper;
using Target10._9.Business.SessionModes.Entities;
using Target10._9.Business.SessionModes.Responses;
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
            CreateMap<SessionMode, UpdateSessionModeByIdResponse>();
        }
    }
}