using Contacts.Domain.Entities;
using MediatR;

namespace Contacts.Application.Dto.Command
{
    public class DeleteInfoTypeDto : IRequest<HandlerResponse<InfoType>>
    {
        public int UUID { get; set; }
    }
}
