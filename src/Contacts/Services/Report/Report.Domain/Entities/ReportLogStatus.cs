using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Report.Domain.Entities
{
    public class ReportLogStatus
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int UUID { get; set; }
        [Required]
        public string Status { get; set; }
    }
}
