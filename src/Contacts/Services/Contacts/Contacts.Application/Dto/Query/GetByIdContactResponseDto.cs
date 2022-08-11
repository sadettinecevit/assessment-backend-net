using Contacts.Domain.Entities;

namespace Contacts.Application.Dto.Command
{
    public class GetByIdContactResponseDto
    {
        public int UUID { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Company { get; set; }
    }
}
