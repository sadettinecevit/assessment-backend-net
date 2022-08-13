using Contacts.Application.Dto;
using Contacts.Application.Dto.Command;
using Contacts.Application.Interfaces.Repositories;
using Contacts.Application.Interfaces.UnitOfWork;
using Contacts.Domain.Entities;
using MediatR;

namespace Contacts.Persistence.CQRS.Handlers.Query
{
    public class GetContactInfoDtoHandler : IRequestHandler<GetContactInfoDto, HandlerResponse<List<GetByIdContactInfoResponseDto>>>
    {
        private readonly IContactInfoRepository _repository;
        public GetContactInfoDtoHandler(IContactInfoRepository repository)
        {
            _repository = repository;
        }
        public async Task<HandlerResponse<List<GetByIdContactInfoResponseDto>>> Handle(GetContactInfoDto request, CancellationToken cancellationToken)
        {
            int skip = (request.PageIndex - 1) * request.DataCount;
            List<ContactInfo> contactInfos = _repository.GetAsync().Result.Skip(skip).Take(request.DataCount).ToList();

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
                            ContactId = contactInfos.ElementAt(i).ContactId,
                            InfoTypeId = contactInfos.ElementAt(i).InfoTypeId,
                            Info = contactInfos.ElementAt(i).Info
                        });
                }
            }

            return handlerResponse;
        }
    }
}
