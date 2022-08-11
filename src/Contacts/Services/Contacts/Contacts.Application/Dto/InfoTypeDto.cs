using MediatR;

namespace Contacts.Application.Dto
{
    public class InfoTypeDto : IRequest<InfoTypeResponse>
    {
        public string Name { get; set; }
    }
}
