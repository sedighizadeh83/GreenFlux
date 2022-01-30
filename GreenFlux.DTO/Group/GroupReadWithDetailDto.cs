using GreenFlux.DTO.ChargeStation;

namespace GreenFlux.DTO.Group
{
    public class GroupReadWithDetailDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Capacity { get; set; }

        public IEnumerable<ChargeStationReadDto> ChargeStations { get; set; }
    }
}
