using System.ComponentModel.DataAnnotations;

namespace GreenFlux.Data.Models
{
    public class Group
    {
        [Required(ErrorMessage = "The field {0} is required.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [Range(0.001, double.MaxValue, ErrorMessage = "The field {0} must be greater than 0.")]
        public decimal Capacity { get; set; }

        public ICollection<ChargeStation> ChargeStationCollection { get; set; }

        public Group()
        {

        }

        public Group(int id, string name, decimal capacity) : this()
        {
            this.Id = id;
            this.Name = name;
            this.Capacity = capacity;
        }
    }
}
