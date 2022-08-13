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
    public class DeleteContactHandler : IRequestHandler<DeleteContactDto, HandlerResponse<Contact>>
    {
        private readonly IContactRepository _repository;
        public DeleteContactHandler(IContactRepository repository)
        {
            _repository = repository;
        }

        public async Task<HandlerResponse<Contact>> Handle(DeleteContactDto deleteContactDto, CancellationToken cancellationToken)
        {
            HandlerResponse<Contact> deleteContactResponse = new HandlerResponse<Contact>();
            Contact result = null;

            try
            {
                Contact obj = await _repository.GetByIdAsync(deleteContactDto.UUID);
                if (obj != null)
                {
                    result = await _repository.Delete(obj);
                    int changedItemCount = await _repository.SaveChanges();

                    deleteContactResponse.IsSuccess = changedItemCount > 0;
                }
            }
            catch (Exception ex)
            {
                deleteContactResponse.Message = ex.Message;
            }

            deleteContactResponse.Message = string.IsNullOrWhiteSpace(deleteContactResponse.Message)
                && deleteContactResponse.IsSuccess ? "Success" : "Unsuccess";

            if (deleteContactResponse.IsSuccess)
            {
                deleteContactResponse.Data = new Contact()
                {
                    UUID = result?.UUID ?? -1,
                    Name = result?.Name ?? string.Empty,
                    Lastname = result?.Lastname ?? string.Empty,
                    Company = result?.Company ?? string.Empty,
                };
            }

            return deleteContactResponse;
        }
    }
}
