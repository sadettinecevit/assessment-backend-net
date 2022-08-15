using Contacts.Application.Dto;
using Contacts.Application.Dto.Command;
using Contacts.Application.Interfaces.Repositories;
using Contacts.Application.Interfaces.UnitOfWork;
using Contacts.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace Contacts.Persistence.CQRS.Handlers.Command
{
    public class UpdateContactHandler : IRequestHandler<UpdateContactDto, HandlerResponse<Contact>>
    {
        private readonly IContactRepository _repository;
        public UpdateContactHandler(IContactRepository repository)
        {
            _repository = repository;
        }

        public async Task<HandlerResponse<Contact>> Handle(UpdateContactDto updateContactDto, CancellationToken cancellationToken)
        {
            HandlerResponse<Contact> updateContactResponse = new HandlerResponse<Contact>();
            Contact result = null;

            try
            {
                if (_repository.GetByIdAsync(updateContactDto.UUID).Result != null)
                {
                    result = await _repository.Update(
                        new Contact
                        {
                            UUID = updateContactDto.UUID,
                            Name = updateContactDto.Name,
                            Lastname = updateContactDto.Lastname,
                            Company = updateContactDto.Company
                        });

                    await _repository.SaveChanges();

                    updateContactResponse.IsSuccess = result != null;
                }
            }
            catch (Exception ex)
            {
                updateContactResponse.Message = ex.Message;
            }

            updateContactResponse.Message = string.IsNullOrWhiteSpace(updateContactResponse.Message) 
                && updateContactResponse.IsSuccess ? "Success" : "Unsuccess";

            updateContactResponse.Data = new Contact()
            {
                UUID = result?.UUID ?? -1,
                Name = result?.Name ?? string.Empty,
                Lastname = result?.Lastname ?? string.Empty,
                Company = result?.Company ?? string.Empty,
            };

            return updateContactResponse;
        }
    }
}
