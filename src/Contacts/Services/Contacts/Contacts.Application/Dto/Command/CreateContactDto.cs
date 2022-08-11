using Contacts.Domain.Entities;
using MediatR;

namespace Contacts.Application.Dto.Command
{
    public class CreateContactDto : IRequest<HandlerResponse<Contact>>
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Company { get; set; }
    }
}
