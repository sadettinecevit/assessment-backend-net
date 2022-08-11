using Contacts.Application.Dto;
using Contacts.Application.Dto.Command;
using Contacts.Application.Interfaces.UnitOfWork;
using Contacts.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace Contacts.Persistence.CQRS.Handlers.Command
{
    public class UpdateContactCommandHandler : IRequestHandler<UpdateContactDto, HandlerResponse<Contact>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateContactCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<HandlerResponse<Contact>> Handle(UpdateContactDto updateContactDto, CancellationToken cancellationToken)
        {
            HandlerResponse<Contact> updateContactResponse = new HandlerResponse<Contact>();
            EntityEntry<Contact> result = null;
            using IDbContextTransaction retVal = await _unitOfWork.BeginTansactionAsync();
            try
            {
                if (_unitOfWork.ContactRepository.GetByIdAsync(updateContactDto.UUID).Result != null)
                {
                    result = await _unitOfWork.ContactRepository.Update(
                        new Contact
                        {
                            UUID = updateContactDto.UUID,
                            Name = updateContactDto.Name,
                            Lastname = updateContactDto.Lastname,
                            Company = updateContactDto.Company
                        });
                    updateContactResponse.IsSuccess = retVal.CommitAsync().IsCompletedSuccessfully;
                }
            }
            catch (Exception ex)
            {
                await retVal.RollbackAsync();
            }

            updateContactResponse.Message = updateContactResponse.IsSuccess ? "Success" : "Unsuccess";

            updateContactResponse.Data = new Contact()
            {
                Name = result?.Entity.Name ?? string.Empty,
                Lastname = result?.Entity.Lastname ?? string.Empty,
                Company = result?.Entity.Company ?? string.Empty,
            };

            return updateContactResponse;
        }
    }
}
