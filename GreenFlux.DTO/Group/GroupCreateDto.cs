using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenFlux.DTO.Group
{
    public class GroupCreateDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0.001, double.MaxValue, ErrorMessage = "The field {0} must be greater than 0.")]
        public decimal Capacity { get; set; }
    }
}
