using Contacts.Application.Dto;
using Contacts.Application.Dto.Command;
using Contacts.Application.Interfaces.Repositories;
using Contacts.Application.Interfaces.UnitOfWork;
using Contacts.Domain.Entities;
using MediatR;

namespace Contacts.Persistence.CQRS.Handlers.Query
{
    public class GetByIdInfoTypeDtoHandler : IRequestHandler<GetByIdInfoTypeDto, HandlerResponse<GetByIdInfoTypeResponseDto>>
    {
        private readonly IInfoTypeRepository _repository;

        public GetByIdInfoTypeDtoHandler(IInfoTypeRepository repository)
        {
            _repository = repository;
        }
        public async Task<HandlerResponse<GetByIdInfoTypeResponseDto>> Handle(GetByIdInfoTypeDto request, CancellationToken cancellationToken)
        {
            InfoType infoTypecontact = await _repository.GetByIdAsync(request.Id);

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
