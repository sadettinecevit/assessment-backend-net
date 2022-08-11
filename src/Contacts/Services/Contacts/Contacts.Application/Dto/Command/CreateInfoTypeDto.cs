using Contacts.Domain.Entities;
using MediatR;

namespace Contacts.Application.Dto.Command
{
    public class CreateInfoTypeDto : IRequest<HandlerResponse<InfoType>>
    {
        public string Name { get; set; }
    }
}
