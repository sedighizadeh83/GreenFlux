using System.ComponentModel.DataAnnotations;

namespace GreenFlux.Data.Models
{
    public class ChargeStation
    {
        [Required(ErrorMessage = "The field {0} is required.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        public int GroupId { get; set; }

        public Group Group { get; set; }

        public ICollection<Connector> ConnectorCollection { get; set; }

        public ChargeStation()
        {
            ConnectorCollection = new List<Connector>();
        }

        public ChargeStation(int id, string name, int groupId) : this()
        {
            this.Id = id;
            this.Name = name;
            this.GroupId = groupId;
        }
    }
}
