using Contacts.Application.Dto;
using Contacts.Application.Interfaces.UnitOfWork;
using Contacts.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Persistence.CQRS.Handlers.Request
{
    public class CreateContactCommandHandler : IRequestHandler<ContactDto, ContactResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateContactCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ContactResponse> Handle(ContactDto createContactDto, CancellationToken cancellationToken)
        {
            ContactResponse createContactResponse = new ContactResponse();
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

            createContactResponse.ContactDto = new ContactDto()
            {
                Name = result?.Entity.Name ?? string.Empty,
                Lastname = result?.Entity.Lastname ?? string.Empty,
                Company = result?.Entity.Company ?? string.Empty,
            };

            return createContactResponse;
        }
    }


}
