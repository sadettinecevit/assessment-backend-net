using Contacts.Application.Dto;
using Contacts.Application.Dto.Command;
using Contacts.Application.Interfaces.Repositories;
using Contacts.Domain.Entities;
using MediatR;

namespace Contacts.Persistence.CQRS.Handlers.Command
{
    public class CreateContactHandler : IRequestHandler<CreateContactDto, HandlerResponse<Contact>>
    {
        private readonly IContactRepository _repository;
        public CreateContactHandler(IContactRepository repository)
        {
            _repository = repository;
        }

        public async Task<HandlerResponse<Contact>> Handle(CreateContactDto createContactDto, CancellationToken cancellationToken)
        {
            HandlerResponse<Contact> createContactResponse = new HandlerResponse<Contact>();
            Contact result = null;
            try
            {
                if (createContactDto != null)
                {
                    result = await _repository.Add(
                        new Contact
                        {
                            Name = createContactDto.Name,
                            Lastname = createContactDto.Lastname,
                            Company = createContactDto.Company
                        });
                    await _repository.SaveChanges();
                    createContactResponse.IsSuccess = result != null;
                }
            }
            catch (Exception ex)
            {
                createContactResponse.Message = ex.Message;
                createContactResponse.Data = null;
            }

            createContactResponse.Message = string.IsNullOrWhiteSpace(createContactResponse.Message)
                && createContactResponse.IsSuccess ? "Success" : "Unsuccess";

            createContactResponse.Data = new Contact()
            {
                UUID = result?.UUID ?? -1,
                Name = result?.Name ?? string.Empty,
                Lastname = result?.Lastname ?? string.Empty,
                Company = result?.Company ?? string.Empty,
            };

            return createContactResponse;
        }
    }
}
