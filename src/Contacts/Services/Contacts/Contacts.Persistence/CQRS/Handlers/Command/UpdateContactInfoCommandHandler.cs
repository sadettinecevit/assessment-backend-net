using Contacts.Application.Dto;
using Contacts.Application.Dto.Command;
using Contacts.Application.Interfaces.UnitOfWork;
using Contacts.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace Contacts.Persistence.CQRS.Handlers.Command
{
    public class UpdateContactInfoCommandHandler : IRequestHandler<UpdateContactInfoDto, HandlerResponse<ContactInfo>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateContactInfoCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<HandlerResponse<ContactInfo>> Handle(UpdateContactInfoDto updateContactInfoDto, CancellationToken cancellationToken)
        {
            HandlerResponse<ContactInfo> updateContactInfoResponse = new HandlerResponse<ContactInfo>();
            EntityEntry<ContactInfo> result = null;
            using IDbContextTransaction retVal = await _unitOfWork.BeginTansactionAsync();
            try
            {
                if (_unitOfWork.ContactInfoRepository.GetByIdAsync(updateContactInfoDto.UUID).Result != null)
                {
                    result = await _unitOfWork.ContactInfoRepository.Update(
                        new ContactInfo
                        {
                            UUID = updateContactInfoDto.UUID,
                            Contact = _unitOfWork.ContactRepository.GetByIdAsync(updateContactInfoDto.ContactId).Result,
                            InfoType = _unitOfWork.InfoTypeRepository.GetByIdAsync(updateContactInfoDto.InfoTypeId).Result,
                            Info = updateContactInfoDto.Info
                        });
                    updateContactInfoResponse.IsSuccess = retVal.CommitAsync().IsCompletedSuccessfully;
                }
            }
            catch (Exception ex)
            {
                await retVal.RollbackAsync();
            }

            updateContactInfoResponse.Message = updateContactInfoResponse.IsSuccess ? "Success" : "Unsuccess";

            updateContactInfoResponse.Data = new ContactInfo()
            {
                Contact = result?.Entity.Contact ?? new(),
                InfoType = result?.Entity.InfoType ?? new(),
                Info = result?.Entity.Info ?? string.Empty
            };

            return updateContactInfoResponse;
        }
    }
}
