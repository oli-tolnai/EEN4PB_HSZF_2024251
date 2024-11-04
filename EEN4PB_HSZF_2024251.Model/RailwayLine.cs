using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EEN4PB_HSZF_2024251.Model
{
    public class RailwayLine
    {
        public RailwayLine(string lineName, string lineNumber)
        {
            Id = Guid.NewGuid().ToString();
            LineName = lineName;
            LineNumber = lineNumber;
            Services = new HashSet<Service>();
        }

        public RailwayLine()
        {
            Services = new HashSet<Service>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required]
        [StringLength(100)]
        public string LineName { get; set; }

        [Required]
        [StringLength(50)]
        public string LineNumber { get; set; }

        public virtual ICollection<Service> Services { get; set; }
    }
}
