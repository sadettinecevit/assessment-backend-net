using Contacts.Domain.Entities;

namespace Contacts.Application.Dto.Command
{
    public class GetByIdContactInfoResponseDto
    {
        public int UUID { get; set; }
        public int ContactId { get; set; }
        public int InfoTypeId { get; set; }
        public string Info { get; set; }
    }
}
