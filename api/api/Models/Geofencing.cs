using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{

    public class Geofencing
    {
        [Column("id")]
        public Guid id { get; set; }

        [Column("latitude")]
        public double latitude { get; set; }

        [Column("longitude")]
        public double longitude { get; set; }

        [Column("horario")]
        public DateTime horario { get; set; }

        [Column("tipo")]
        public string tipo { get; set; }
    }
}
