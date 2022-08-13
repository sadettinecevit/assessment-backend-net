using Contacts.Application.Dto;
using Contacts.Application.Dto.Command;
using Contacts.Application.Interfaces.Repositories;
using Contacts.Application.Interfaces.UnitOfWork;
using Contacts.Domain.Entities;
using MediatR;

namespace Contacts.Persistence.CQRS.Handlers.Query
{
    public class GetContactDtoHandler : IRequestHandler<GetContactDto, HandlerResponse<List<GetByIdContactResponseDto>>>
    {
        private readonly IContactRepository _repository;
        public GetContactDtoHandler(IContactRepository repository)
        {
            _repository = repository;
        }
        public async Task<HandlerResponse<List<GetByIdContactResponseDto>>> Handle(GetContactDto request, CancellationToken cancellationToken)
        {
            int skip = (request.PageIndex - 1) * request.DataCount;
            List<Contact> contacts = _repository.GetAsync().Result.Skip(skip).Take(request.DataCount).ToList();

            HandlerResponse<List<GetByIdContactResponseDto>> handlerResponse = new HandlerResponse<List<GetByIdContactResponseDto>>();
            handlerResponse.IsSuccess = contacts != null;

            if (contacts != null)
            {
                handlerResponse.Data = new();
                for (int i = 0; i < contacts.Count; i++)
                {
                    handlerResponse.Data.Add(
                        new GetByIdContactResponseDto()
                        {
                            UUID = contacts.ElementAt(i).UUID,
                            Name = contacts.ElementAt(i).Name,
                            Lastname = contacts.ElementAt(i).Lastname,
                            Company = contacts.ElementAt(i).Company,
                        });
                }
            }

            return handlerResponse;
        }
    }
}
