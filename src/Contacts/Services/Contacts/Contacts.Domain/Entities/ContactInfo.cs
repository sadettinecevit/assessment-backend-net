using Contacts.Domain.Common;

namespace Contacts.Domain.Entities
{
    public class ContactInfo : IBaseEntity
    {
        public int UUID { get; set; }
        public Contact Contact { get; set; }
        public InfoType InfoType { get; set; }
        public string Info { get; set; }
    }
}
