using AutoMapper;
using Target10._9.Business.Sessions.Entities;
using Target10._9.Business.Sessions.Responses;

namespace Target10._9.Business.Sessions.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Session, GetSessionsResponse>();
        }
    }
}