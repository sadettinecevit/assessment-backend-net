using Contacts.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contacts.Domain.Entities
{
    public class ContactInfo : IBaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int UUID { get; set; }
        [Required]
        [ForeignKey("Contact")]
        public int ContactId { get; set; }
        [Required]
        [ForeignKey("InfoType")]
        public int InfoTypeId { get; set; }
        [Required]
        public string Info { get; set; }
    }
}
