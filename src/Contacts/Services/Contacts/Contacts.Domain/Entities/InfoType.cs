using Contacts.Domain.Common;

namespace Contacts.Domain.Entities
{
    public class InfoType : IBaseEntity
    {
        public int UUID { get; set; }
        public string Name { get; set; }
    }
}
