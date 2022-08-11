using MediatR;

namespace Contacts.Application.Dto.Command
{
    public class GetByIdInfoTypeDto : IRequest<HandlerResponse<GetByIdInfoTypeResponseDto>>
    {
        public int Id { get; set; }
    }
}
