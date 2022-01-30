using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenFlux.DTO.Connector
{
    public class ConnectorCreateIndirectDto
    {
        [Required]
        [Range(1, 5, ErrorMessage = "The field {0} must be between {1} and {2}.")]
        public int Id { get; set; }

        [Required]
        [Range(0.001, double.MaxValue, ErrorMessage = "The field {0} must be greater than 0.")]
        public decimal MaxCurrent { get; set; }
    }
}
