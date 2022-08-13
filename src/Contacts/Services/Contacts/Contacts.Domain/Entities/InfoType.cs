using Contacts.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contacts.Domain.Entities
{
    public class InfoType : IBaseEntity
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Key]
        public int UUID { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
