using Contacts.Domain.Entities;
using MediatR;

namespace Contacts.Application.Dto.Command
{
    public class DeleteContactDto : IRequest<HandlerResponse<Contact>>
    {
        public int UUID { get; set; }
    }
}
