using Contacts.Domain.Entities;
using MediatR;

namespace Contacts.Application.Dto.Command
{
    public class UpdateInfoTypeDto : IRequest<HandlerResponse<InfoType>>
    {
        public int UUID { get; set; }
        public string Name { get; set; }
    }
}
