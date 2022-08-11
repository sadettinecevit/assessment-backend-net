using Contacts.Domain.Entities;

namespace Contacts.Application.Dto.Command
{
    public class GetByIdContactInfoResponseDto
    {
        public int UUID { get; set; }
        public Contact Contact { get; set; }
        public InfoType InfoType { get; set; }
        public string Info { get; set; }
    }
}
