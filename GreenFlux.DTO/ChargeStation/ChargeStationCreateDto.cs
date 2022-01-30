using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GreenFlux.DTO.Connector;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenFlux.DTO.ChargeStation
{
    public class ChargeStationCreateDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int GroupId { get; set; }

        [Required]
        public IEnumerable<ConnectorCreateIndirectDto> connectors { get; set; }
    }
}
