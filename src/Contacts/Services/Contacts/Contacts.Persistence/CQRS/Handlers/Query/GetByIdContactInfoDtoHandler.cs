using Contacts.Application.Dto;
using Contacts.Application.Dto.Command;
using Contacts.Application.Interfaces.Repositories;
using Contacts.Application.Interfaces.UnitOfWork;
using Contacts.Domain.Entities;
using MediatR;

namespace Contacts.Persistence.CQRS.Handlers.Query
{
    public class GetByIdContactInfoDtoHandler : IRequestHandler<GetByIdContactInfoDto, HandlerResponse<GetByIdContactInfoResponseDto>>
    {
        private readonly IContactInfoRepository _repository;
        public GetByIdContactInfoDtoHandler(IContactInfoRepository repository)
        {
            _repository = repository;
        }
        public async Task<HandlerResponse<GetByIdContactInfoResponseDto>> Handle(GetByIdContactInfoDto request, CancellationToken cancellationToken)
        {
            ContactInfo contactInfo = await _repository.GetByIdAsync(request.Id);

            HandlerResponse<GetByIdContactInfoResponseDto> handlerResponse = new HandlerResponse<GetByIdContactInfoResponseDto>();
            handlerResponse.IsSuccess = contactInfo != null;

            if (contactInfo != null)
            {
                handlerResponse.Data = new GetByIdContactInfoResponseDto()
                {
                    UUID = request.Id,
                    ContactId = contactInfo.ContactId,
                    InfoTypeId = contactInfo.InfoTypeId,
                    Info = contactInfo.Info
                };
            }

            return handlerResponse;
        }
    }
}
