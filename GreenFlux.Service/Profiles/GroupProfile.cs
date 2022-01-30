using AutoMapper;
using GreenFlux.DTO.Group;
using GreenFlux.Data.Models;

namespace GreenFlux.Service.Profiles
{
    public class GroupProfile : Profile
    {
        public GroupProfile()
        {
            //Source --> Target
            CreateMap<Group, GroupReadDto>()
                .ForMember(s => s.Id, c => c.MapFrom(m => m.Id))
                .ForMember(s => s.Name, c => c.MapFrom(m => m.Name))
                .ForMember(s => s.Capacity, c => c.MapFrom(m => m.Capacity));
            CreateMap<Group, GroupReadWithDetailDto>()
                .ForMember(s => s.Id, c => c.MapFrom(m => m.Id))
                .ForMember(s => s.Name, c => c.MapFrom(m => m.Name))
                .ForMember(s => s.Capacity, c => c.MapFrom(m => m.Capacity))
                .ForMember(s => s.ChargeStations, c => c.MapFrom(m => m.ChargeStationCollection));
            CreateMap<GroupCreateDto, Group>();
            CreateMap<GroupUpdateDto, Group>();
        }
    }
}
