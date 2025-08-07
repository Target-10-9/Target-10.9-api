using AutoMapper;
using Target10._9.Business.Points.Entities;
using Target10._9.Business.Points.Responses;

namespace Target10._9.Business.Points.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Point, GetPointsResponse>();
            
            CreateMap<Point, AddPointResponse>();
        }
    }
}