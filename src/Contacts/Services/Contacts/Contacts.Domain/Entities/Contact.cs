using Contacts.Domain.Common;

namespace Contacts.Domain.Entities
{
    public class Contact : IBaseEntity
    {
        public int UUID { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Company { get; set; }
    }
}
