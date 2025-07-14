using AutoMapper;
using Target10._9.Business.ModeDetails.Entities;
using Target10._9.Business.ModeDetails.Responses;

namespace Target10._9.Business.ModeDetails.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ModeDetail, GetModeDetailsResponse>();
            CreateMap<ModeDetail, GetModeDetailByIdResponse>();

            CreateMap<ModeDetail, AddModeDetailResponse>();
            CreateMap<ModeDetail, UpdateModeDetailByIdResponse>();
        }
    }
}