using MediatR;

namespace Contacts.Application.Dto.Command
{
    public class GetInfoTypeDto : FilteredPageQuery, IRequest<HandlerResponse<List<GetByIdInfoTypeResponseDto>>>
    {
    }
}
