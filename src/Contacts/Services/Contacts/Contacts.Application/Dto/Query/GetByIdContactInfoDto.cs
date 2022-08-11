using MediatR;

namespace Contacts.Application.Dto.Command
{
    public class GetByIdContactInfoDto : IRequest<HandlerResponse<GetByIdContactInfoResponseDto>>
    {
        public int Id { get; set; }
    }
}
