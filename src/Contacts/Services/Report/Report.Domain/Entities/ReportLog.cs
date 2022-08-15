using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Report.Domain.Entities
{
    public class ReportLog
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int UUID { get; set; }
        [Required]
        public DateTime RequestDate { get; set; } = DateTime.Now;
        [Required]
        [ForeignKey("ReportLogStatus")]
        public int StatusID { get; set; }
    }
}
