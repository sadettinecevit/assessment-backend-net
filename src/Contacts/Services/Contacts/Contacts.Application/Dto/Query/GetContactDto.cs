using MediatR;

namespace Contacts.Application.Dto.Command
{
    public class GetContactDto : FilteredPageQuery, IRequest<HandlerResponse<List<GetByIdContactResponseDto>>>
    {
    }
}
