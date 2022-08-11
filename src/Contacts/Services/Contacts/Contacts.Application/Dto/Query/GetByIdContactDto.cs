using Contacts.Application.Dto.Command;
using MediatR;

namespace Contacts.Application.Dto
{
    public class GetByIdContactDto : IRequest<HandlerResponse<GetByIdContactResponseDto>>
    {
        public int Id { get; set; }
    }
}
