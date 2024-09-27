using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IBKSDevScenarioAPI.Models
{
    [Table("Inquries", Schema = "gov")]
    public class Inquries
    {

        [Key]
        public int Id { get; set; }
        [ForeignKey("Application")]
        public int? ApplicationId { get; set; }
        public string? SendToPerson { get; set; }
        public string? SendToRole { get; set; }
        public int? SendToPersonId { get; set; }
        public string? Subject { get; set; }
        public string? Inquiry { get; set; }
        public string? Response { get; set; }
        public DateTime? AskedDt { get; set; }
        public DateTime? CompletedDt { get; set; }

     
    }
}
