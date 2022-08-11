using Contacts.Application.Dto;
using Contacts.Application.Dto.Command;
using Contacts.Application.Interfaces.UnitOfWork;
using Contacts.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace Contacts.Persistence.CQRS.Handlers.Command
{
    public class CreateContactInfoCommandHandler : IRequestHandler<CreateContactInfoDto, HandlerResponse<ContactInfo>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateContactInfoCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<HandlerResponse<ContactInfo>> Handle(CreateContactInfoDto createContactInfoDto, CancellationToken cancellationToken)
        {
            HandlerResponse<ContactInfo> createContactInfoResponse = new HandlerResponse<ContactInfo>();
            EntityEntry<ContactInfo> result = null;
            using IDbContextTransaction retVal = await _unitOfWork.BeginTansactionAsync();
            try
            {
                result = await _unitOfWork.ContactInfoRepository.Add(
                    new ContactInfo
                    {
                        Contact = _unitOfWork.ContactRepository.GetByIdAsync(createContactInfoDto.ContactId).Result,
                        InfoType = _unitOfWork.InfoTypeRepository.GetByIdAsync(createContactInfoDto.InfoTypeId).Result,
                        Info = createContactInfoDto.Info
                    });

                createContactInfoResponse.IsSuccess = retVal.CommitAsync().IsCompletedSuccessfully;
            }
            catch (Exception ex)
            {
                await retVal.RollbackAsync();
            }

            createContactInfoResponse.Message = createContactInfoResponse.IsSuccess ? "Success" : "Unsuccess";

            createContactInfoResponse.Data = new ContactInfo()
            {
                UUID = result?.Entity.UUID ?? -1,
                Contact = result?.Entity.Contact ?? new(),
                InfoType = result?.Entity.InfoType ?? new(),
                Info = result?.Entity.Info ?? string.Empty
            };

            return createContactInfoResponse;
        }
    }
}
