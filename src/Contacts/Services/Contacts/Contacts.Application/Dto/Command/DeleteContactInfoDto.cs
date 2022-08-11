using Contacts.Domain.Entities;
using MediatR;

namespace Contacts.Application.Dto.Command
{
    public class DeleteContactInfoDto : IRequest<HandlerResponse<ContactInfo>>
    {
        public int UUID { get; set; }
    }
}
