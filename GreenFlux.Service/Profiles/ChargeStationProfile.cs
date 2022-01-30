using AutoMapper;
using GreenFlux.DTO.ChargeStation;
using GreenFlux.Data.Models;

namespace GreenFlux.Service.Profiles
{
    public class ChargeStationProfile : Profile
    {
        public ChargeStationProfile()
        {
            //Source --> Target
            CreateMap<ChargeStation, ChargeStationReadDto>()
                .ForMember(s => s.Id, c => c.MapFrom(m => m.Id))
                .ForMember(s => s.Name, c => c.MapFrom(m => m.Name))
                .ForMember(s => s.Connectors, c => c.MapFrom(m => m.ConnectorCollection));
            CreateMap<ChargeStation, ChargeStationReadWithDetailDto>()
                .ForMember(s => s.Id, c => c.MapFrom(m => m.Id))
                .ForMember(s => s.Name, c => c.MapFrom(m => m.Name))
                .ForMember(s => s.Group, c => c.MapFrom(m => m.Group))
                .ForMember(s => s.Connectors, c => c.MapFrom(m => m.ConnectorCollection));
            CreateMap<ChargeStation, ChargeStationReadWithGroupDto>()
                .ForMember(s => s.Id, c => c.MapFrom(m => m.Id))
                .ForMember(s => s.Name, c => c.MapFrom(m => m.Name))
                .ForMember(s => s.Group, c => c.MapFrom(m => m.Group));
            CreateMap<ChargeStationCreateDto, ChargeStation>()
                .ForMember(s => s.Name, c => c.MapFrom(m => m.Name))
                .ForMember(s => s.GroupId, c => c.MapFrom(m => m.GroupId));
            CreateMap<ChargeStationUpdateDto, ChargeStation>()
                .ForMember(s => s.Id, c => c.MapFrom(m => m.Id))
                .ForMember(s => s.Name, c => c.MapFrom(m => m.Name))
                .ForMember(s => s.GroupId, c => c.MapFrom(m => m.GroupId));
            CreateMap<ChargeStationNewGroupUpdateDto, ChargeStation>()
                .ForMember(s => s.Id, c => c.MapFrom(m => m.Id))
                .ForMember(s => s.Name, c => c.MapFrom(m => m.Name))
                .ForMember(s => s.GroupId, c => c.MapFrom(m => m.GroupId));
        }
    }
}
