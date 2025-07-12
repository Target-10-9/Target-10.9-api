using AutoMapper;
using Target10._9.Business.Users.Entities;
using Target10._9.Business.Users.Responses;

namespace Target10._9.Business.Users.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, GetUserResponse>();
        }
    }
}