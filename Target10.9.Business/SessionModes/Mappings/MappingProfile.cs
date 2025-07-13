using AutoMapper;
using Target10._9.Business.SessionModes.Entities;
using Target10._9.Business.SessionModes.Responses;

namespace Target10._9.Business.SessionModes.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SessionMode, GetSessionModesResponse>();
        }
    }
}