using AutoMapper;
using Target10._9.Business.WeaponDetails.Entities;
using Target10._9.Business.WeaponDetails.Responses;

namespace Target10._9.Business.WeaponDetails.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<WeaponDetail, GetWeaponDetailsResponse>();
            CreateMap<WeaponDetail, GetWeaponDetailByIdResponse>();
            
            CreateMap<WeaponDetail, AddWeaponDetailResponse>();
            CreateMap<WeaponDetail, UpdateWeaponDetailByIdResponse>();
        }
    }
}