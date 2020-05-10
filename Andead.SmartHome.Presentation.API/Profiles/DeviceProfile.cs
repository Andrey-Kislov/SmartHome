using AutoMapper;
using Andead.SmartHome.Presentation.API.Models;
using Andead.SmartHome.UnitOfWork.Entities;

namespace Andead.SmartHome.Presentation.API.Profiles
{
    public class DeviceProfile : Profile
    {
        public DeviceProfile()
        {
            CreateMap<Device, DeviceDto>()
                .ForMember(x => x.ImageUrl, x => x.MapFrom(y => y.Model.ImageUrl))
                .ReverseMap();
        }
    }
}
