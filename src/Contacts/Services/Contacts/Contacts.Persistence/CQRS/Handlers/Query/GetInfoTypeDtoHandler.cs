using Contacts.Application.Dto;
using Contacts.Application.Dto.Command;
using Contacts.Application.Interfaces.Repositories;
using Contacts.Domain.Entities;
using MediatR;

namespace Contacts.Persistence.CQRS.Handlers.Query
{
    public class GetInfoTypeDtoHandler : IRequestHandler<GetInfoTypeDto, HandlerResponse<List<GetByIdInfoTypeResponseDto>>>
    {
        private readonly IInfoTypeRepository _repository;
        public GetInfoTypeDtoHandler(IInfoTypeRepository repository)
        {
            _repository = repository;
        }
        public async Task<HandlerResponse<List<GetByIdInfoTypeResponseDto>>> Handle(GetInfoTypeDto request, CancellationToken cancellationToken)
        {
            int skip = (request.PageIndex - 1) * request.DataCount;
            List<InfoType> infoTypes = _repository.GetAsync().Result.Skip(skip).Take(request.DataCount).ToList();

            HandlerResponse<List<GetByIdInfoTypeResponseDto>> handlerResponse = new HandlerResponse<List<GetByIdInfoTypeResponseDto>>();
            handlerResponse.IsSuccess = infoTypes != null;

            if (infoTypes != null)
            {
                handlerResponse.Data = new();
                for (int i = 0; i < infoTypes.Count; i++)
                {
                    handlerResponse.Data.Add(
                        new GetByIdInfoTypeResponseDto()
                        {
                            UUID = infoTypes.ElementAt(i).UUID,
                            Name = infoTypes.ElementAt(i).Name
                        });
                }
            }

            return handlerResponse;
        }
    }
}
