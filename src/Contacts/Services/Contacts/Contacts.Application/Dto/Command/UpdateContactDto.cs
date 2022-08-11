using Contacts.Domain.Entities;
using MediatR;

namespace Contacts.Application.Dto.Command
{
    public class UpdateContactDto : IRequest<HandlerResponse<Contact>>
    {
        public int UUID { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Company { get; set; }
    }
}
