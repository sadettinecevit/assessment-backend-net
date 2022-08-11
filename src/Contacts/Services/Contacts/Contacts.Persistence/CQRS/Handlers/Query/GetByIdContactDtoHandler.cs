using Contacts.Application.Dto;
using Contacts.Application.Dto.Command;
using Contacts.Application.Interfaces.UnitOfWork;
using Contacts.Domain.Entities;
using MediatR;

namespace Contacts.Persistence.CQRS.Handlers.Query
{
    public class GetByIdContactDtoHandler : BaseHandler, IRequestHandler<GetByIdContactDto, HandlerResponse<GetByIdContactResponseDto>>
    {
        public GetByIdContactDtoHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        public async Task<HandlerResponse<GetByIdContactResponseDto>> Handle(GetByIdContactDto request, CancellationToken cancellationToken)
        {
            Contact contact = await _unitOfWork.ContactRepository.GetByIdAsync(request.Id);

            HandlerResponse<GetByIdContactResponseDto> handlerResponse = new HandlerResponse<GetByIdContactResponseDto>();
            handlerResponse.IsSuccess = contact != null;

            if (contact != null)
            {
                handlerResponse.Data = new GetByIdContactResponseDto()
                {
                    UUID = request.Id,
                    Name = contact.Name,
                    Lastname = contact.Lastname,
                    Company = contact.Company,
                };
            }

            return handlerResponse;
        }
    }
}
