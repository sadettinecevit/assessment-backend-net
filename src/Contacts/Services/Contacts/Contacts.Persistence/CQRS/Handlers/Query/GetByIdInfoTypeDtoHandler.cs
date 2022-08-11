using Contacts.Application.Dto;
using Contacts.Application.Dto.Command;
using Contacts.Application.Interfaces.UnitOfWork;
using Contacts.Domain.Entities;
using MediatR;

namespace Contacts.Persistence.CQRS.Handlers.Query
{
    public class GetByIdInfoTypeDtoHandler : BaseHandler, IRequestHandler<GetByIdInfoTypeDto, HandlerResponse<GetByIdInfoTypeResponseDto>>
    {
        public GetByIdInfoTypeDtoHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        public async Task<HandlerResponse<GetByIdInfoTypeResponseDto>> Handle(GetByIdInfoTypeDto request, CancellationToken cancellationToken)
        {
            InfoType infoTypecontact = await _unitOfWork.InfoTypeRepository.GetByIdAsync(request.Id);

            HandlerResponse<GetByIdInfoTypeResponseDto> handlerResponse = new HandlerResponse<GetByIdInfoTypeResponseDto>();
            handlerResponse.IsSuccess = infoTypecontact != null;

            if (infoTypecontact != null)
            {
                handlerResponse.Data = new GetByIdInfoTypeResponseDto()
                {
                    UUID = request.Id,
                    Name = infoTypecontact.Name
                };
            }

            return handlerResponse;
        }
    }
}
