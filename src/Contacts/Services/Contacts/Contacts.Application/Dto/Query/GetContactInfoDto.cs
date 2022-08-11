using MediatR;

namespace Contacts.Application.Dto.Command
{
    public class GetContactInfoDto : FilteredPageQuery, IRequest<HandlerResponse<List<GetByIdContactInfoResponseDto>>>
    {
    }
}
