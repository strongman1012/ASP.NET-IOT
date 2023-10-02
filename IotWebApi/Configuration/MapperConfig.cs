using AutoMapper;
using IotWebApi.Dto;
using IotWebApi.Entities;

namespace IotWebApi.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<UserDto,
                UserEto>().ReverseMap();
            CreateMap<RoleDto,
                RoleEto>().ReverseMap();
            CreateMap<CategoryDto,
                CategoryEto>().ReverseMap();
            CreateMap<DeviceModelDto,
                DeviceModelEto>().ReverseMap();
            CreateMap<AuditLogDto,
                AuditLogEto>().ReverseMap();
            CreateMap<OrganisationDto,
                OrganisationEto>().ReverseMap();
        }
    }
}
