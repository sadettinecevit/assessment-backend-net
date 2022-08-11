using Contacts.Application.Dto;
using Contacts.Application.Dto.Command;
using Contacts.Application.Interfaces.UnitOfWork;
using Contacts.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace Contacts.Persistence.CQRS.Handlers.Command
{
    public class DeleteContactCommandHandler : IRequestHandler<DeleteContactDto, HandlerResponse<Contact>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteContactCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<HandlerResponse<Contact>> Handle(DeleteContactDto deleteContactDto, CancellationToken cancellationToken)
        {
            HandlerResponse<Contact> deleteContactResponse = new HandlerResponse<Contact>();
            EntityEntry<Contact> result = null;

            using IDbContextTransaction retVal = await _unitOfWork.BeginTansactionAsync();
            try
            {
                Contact obj = await _unitOfWork.ContactRepository.GetByIdAsync(deleteContactDto.UUID);
                if (obj != null)
                {
                    result = await _unitOfWork.ContactRepository.Delete(obj);
                    deleteContactResponse.IsSuccess = retVal.CommitAsync().IsCompletedSuccessfully;
                }
            }
            catch (Exception ex)
            {
                await retVal.RollbackAsync();
            }

            deleteContactResponse.Message = deleteContactResponse.IsSuccess ? "Success" : "Unsuccess";

            if (deleteContactResponse.IsSuccess)
            {
                deleteContactResponse.Data = new Contact()
                {
                    Name = result?.Entity.Name ?? string.Empty,
                    Lastname = result?.Entity.Lastname ?? string.Empty,
                    Company = result?.Entity.Company ?? string.Empty,
                };
            }

            return deleteContactResponse;
        }
    }
}
