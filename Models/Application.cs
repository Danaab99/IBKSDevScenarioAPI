using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IBKSDevScenarioAPI.Models
{
    [Table("Application", Schema = "gov")]
    public class Application
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string? AppStatus { get; set; }

        public string? ProjectRef { get; set; }

        [StringLength(100)]
        public string? ProjectName { get; set; }

        [StringLength(200)]
        public string? ProjectLocation { get; set; }

        public DateTime? OpenDt { get; set; }

        public DateTime? StartDt { get; set; }

        public DateTime? CompletedDt { get; set; }

        public decimal? ProjectValue { get; set; }

        [ForeignKey("StatusLevel")]
        public int StatusId { get; set; }

        public string? Notes { get; set; }

        public DateTime? Modified { get; set; }

        public bool? isDeleted { get; set; }
    }
}
