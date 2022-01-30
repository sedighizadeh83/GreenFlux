using GreenFlux.DTO.ChargeStation;

namespace GreenFlux.DTO.Connector
{
    public class ConnectorReadWithDetailDto
    {
        public int Id { get; set; }

        public ChargeStationReadDto ChargeStation { get; set; }

        public decimal MaxCurrent { get; set; }
    }
}
