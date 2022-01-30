using AutoMapper;
using GreenFlux.DTO.Connector;
using GreenFlux.Data.Models;

namespace GreenFlux.Service.Profiles
{
    public class ConnectorProfile : Profile
    {
        public ConnectorProfile()
        {
            //Source --> Target
            CreateMap<Connector, ConnectorReadDto>()
                .ForMember(s => s.Id, c => c.MapFrom(m => m.Id))
                .ForMember(s => s.MaxCurrent, c => c.MapFrom(m => m.MaxCurrent));
            CreateMap<Connector, ConnectorReadWithDetailDto>()
                .ForMember(s => s.Id, c => c.MapFrom(m => m.Id))
                .ForMember(s => s.MaxCurrent, c => c.MapFrom(m => m.MaxCurrent))
                .ForMember(s => s.ChargeStation, c => c.MapFrom(m => m.ChargeStation));
            CreateMap<ConnectorCreateDto, Connector>();
            CreateMap<ConnectorCreateIndirectDto, Connector>()
                .ForMember(s => s.Id, c => c.MapFrom(m => m.Id))
                .ForMember(s => s.MaxCurrent, c => c.MapFrom(m => m.MaxCurrent));
            CreateMap<ConnectorUpdateDto, Connector>();
        }
    }
}
