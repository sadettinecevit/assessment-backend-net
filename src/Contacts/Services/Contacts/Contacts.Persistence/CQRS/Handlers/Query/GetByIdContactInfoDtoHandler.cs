using Contacts.Application.Dto;
using Contacts.Application.Dto.Command;
using Contacts.Application.Interfaces.UnitOfWork;
using Contacts.Domain.Entities;
using MediatR;

namespace Contacts.Persistence.CQRS.Handlers.Query
{
    public class GetByIdContactInfoDtoHandler : BaseHandler, IRequestHandler<GetByIdContactInfoDto, HandlerResponse<GetByIdContactInfoResponseDto>>
    {
        public GetByIdContactInfoDtoHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        public async Task<HandlerResponse<GetByIdContactInfoResponseDto>> Handle(GetByIdContactInfoDto request, CancellationToken cancellationToken)
        {
            ContactInfo contactInfo = await _unitOfWork.ContactInfoRepository.GetByIdAsync(request.Id);

            HandlerResponse<GetByIdContactInfoResponseDto> handlerResponse = new HandlerResponse<GetByIdContactInfoResponseDto>();
            handlerResponse.IsSuccess = contactInfo != null;

            if (contactInfo != null)
            {
                handlerResponse.Data = new GetByIdContactInfoResponseDto()
                {
                    UUID = request.Id,
                    Contact = contactInfo.Contact,
                    InfoType = contactInfo.InfoType,
                    Info = contactInfo.Info
                };
            }

            return handlerResponse;
        }
    }
}
