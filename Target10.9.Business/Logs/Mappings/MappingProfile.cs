using AutoMapper;
using Target10._9.Business.Logs.Entities;
using Target10._9.Business.Logs.Responses;

namespace Target10._9.Business.Logs.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Log, GetLogsResponse>();
            CreateMap<Log, GetLogByIdResponse>();
        }
    }
}