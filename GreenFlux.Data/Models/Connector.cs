using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenFlux.Data.Models
{
    public class Connector
    {
        [Required(ErrorMessage = "The field {0} is required.")]
        [Range(1,5, ErrorMessage = "The field {0} must be between {1} and {2}.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        public int ChargeStationId { get; set; }

        public ChargeStation ChargeStation { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [Range(0.001, double.MaxValue, ErrorMessage = "The field {0} must be greater than 0.")]
        public decimal MaxCurrent { get; set; }

        public Connector()
        {

        }

        public Connector(int id, int chargeStationId, decimal maxCurrent) : this()
        {
            this.Id = id;
            this.ChargeStationId = chargeStationId;
            this.MaxCurrent = maxCurrent;
        }
    }
}
