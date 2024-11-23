using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EEN4PB_HSZF_2024251.Model
{
    public class RailwayLine
    {
        public RailwayLine(string lineName, string lineNumber)
        {
            LineName = lineName;
            LineNumber = lineNumber;
            Services = new HashSet<Service>();
        }

        public RailwayLine()
        {
            Services = new HashSet<Service>();
        }

        [Required]
        [StringLength(100)]
        public string LineName { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [StringLength(50)]
        public string LineNumber { get; set; }

        public virtual ICollection<Service> Services { get; set; }
    }
}
