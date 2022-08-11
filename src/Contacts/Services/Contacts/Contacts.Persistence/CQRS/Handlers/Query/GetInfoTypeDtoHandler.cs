using Contacts.Application.Dto;
using Contacts.Application.Dto.Command;
using Contacts.Application.Interfaces.UnitOfWork;
using Contacts.Domain.Entities;
using MediatR;

namespace Contacts.Persistence.CQRS.Handlers.Query
{
    public class GetInfoTypeDtoHandler : BaseHandler, IRequestHandler<GetInfoTypeDto, HandlerResponse<List<GetByIdInfoTypeResponseDto>>>
    {
        public GetInfoTypeDtoHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        public async Task<HandlerResponse<List<GetByIdInfoTypeResponseDto>>> Handle(GetInfoTypeDto request, CancellationToken cancellationToken)
        {
            List<InfoType> infoTypes = await _unitOfWork.InfoTypeRepository.GetAsync();

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
