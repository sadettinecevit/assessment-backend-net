using Contacts.Application.Dto;
using Contacts.Application.Dto.Command;
using Contacts.Application.Interfaces.UnitOfWork;
using Contacts.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace Contacts.Persistence.CQRS.Handlers.Command
{
    public class CreateContactCommandHandler : IRequestHandler<CreateContactDto, HandlerResponse<Contact>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateContactCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<HandlerResponse<Contact>> Handle(CreateContactDto createContactDto, CancellationToken cancellationToken)
        {
            HandlerResponse<Contact> createContactResponse = new HandlerResponse<Contact>();
            EntityEntry<Contact> result = null;
            using IDbContextTransaction retVal = await _unitOfWork.BeginTansactionAsync();
            try
            {
                result = await _unitOfWork.ContactRepository.Add(
                    new Contact
                    {
                        Name = createContactDto.Name,
                        Lastname = createContactDto.Lastname,
                        Company = createContactDto.Company
                    });
                createContactResponse.IsSuccess = retVal.CommitAsync().IsCompletedSuccessfully;
            }
            catch (Exception ex)
            {
                await retVal.RollbackAsync();
            }

            createContactResponse.Message = createContactResponse.IsSuccess ? "Success" : "Unsuccess";

            createContactResponse.Data = new Contact()
            {
                Name = result?.Entity.Name ?? string.Empty,
                Lastname = result?.Entity.Lastname ?? string.Empty,
                Company = result?.Entity.Company ?? string.Empty,
            };

            return createContactResponse;
        }
    }
}
