using Contacts.Domain.Entities;
using MediatR;

namespace Contacts.Application.Dto.Command
{
    public class CreateContactInfoDto : IRequest<HandlerResponse<ContactInfo>>
    {
        public int ContactId { get; set; }
        public int InfoTypeId { get; set; }
        public string Info { get; set; }
    }
}
