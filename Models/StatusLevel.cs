using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IBKSDevScenarioAPI.Models
{
    public class StatusLevel
    {
        [Key]
       
        public int Id { get; set; }

        public string? StatusName { get; set; }
    }
}
