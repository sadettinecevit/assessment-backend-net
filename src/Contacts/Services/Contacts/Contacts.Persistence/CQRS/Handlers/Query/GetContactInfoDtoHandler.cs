using Contacts.Application.Dto;
using Contacts.Application.Dto.Command;
using Contacts.Application.Interfaces.UnitOfWork;
using Contacts.Domain.Entities;
using MediatR;

namespace Contacts.Persistence.CQRS.Handlers.Query
{
    public class GetContactInfoDtoHandler : BaseHandler, IRequestHandler<GetContactInfoDto, HandlerResponse<List<GetByIdContactInfoResponseDto>>>
    {
        public GetContactInfoDtoHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        public async Task<HandlerResponse<List<GetByIdContactInfoResponseDto>>> Handle(GetContactInfoDto request, CancellationToken cancellationToken)
        {
            List<ContactInfo> contactInfos = await _unitOfWork.ContactInfoRepository.GetAsync();

            HandlerResponse<List<GetByIdContactInfoResponseDto>> handlerResponse = new HandlerResponse<List<GetByIdContactInfoResponseDto>>();
            handlerResponse.IsSuccess = contactInfos != null;

            if (contactInfos != null)
            {
                handlerResponse.Data = new();
                for (int i = 0; i < contactInfos.Count; i++)
                {
                    handlerResponse.Data.Add(
                        new GetByIdContactInfoResponseDto()
                        {
                            UUID = contactInfos.ElementAt(i).UUID,
                            Contact = contactInfos.ElementAt(i).Contact,
                            InfoType = contactInfos.ElementAt(i).InfoType,
                            Info = contactInfos.ElementAt(i).Info
                        });
                }
            }

            return handlerResponse;
        }
    }
}
