using GreenFlux.DTO.Group;
using GreenFlux.DTO.Connector;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenFlux.DTO.ChargeStation
{
    public class ChargeStationReadWithGroupDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public GroupReadDto Group { get; set; }
    }
}
