using Contacts.Application.Dto;
using Contacts.Application.Dto.Command;
using Contacts.Application.Interfaces.UnitOfWork;
using Contacts.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace Contacts.Persistence.CQRS.Handlers.Command
{
    public class DeleteContactInfoCommandHandler : IRequestHandler<DeleteContactInfoDto, HandlerResponse<ContactInfo>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteContactInfoCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<HandlerResponse<ContactInfo>> Handle(DeleteContactInfoDto deleteContactInfoDto, CancellationToken cancellationToken)
        {
            HandlerResponse<ContactInfo> deleteContactInfoResponse = new HandlerResponse<ContactInfo>();
            EntityEntry<ContactInfo> result = null;

            using IDbContextTransaction retVal = await _unitOfWork.BeginTansactionAsync();
            try
            {
                ContactInfo obj = await _unitOfWork.ContactInfoRepository.GetByIdAsync(deleteContactInfoDto.UUID);
                if (obj != null)
                {
                    result = await _unitOfWork.ContactInfoRepository.Delete(obj);
                    deleteContactInfoResponse.IsSuccess = retVal.CommitAsync().IsCompletedSuccessfully;
                }
            }
            catch (Exception ex)
            {
                await retVal.RollbackAsync();
            }

            deleteContactInfoResponse.Message = deleteContactInfoResponse.IsSuccess ? "Success" : "Unsuccess";

            if (deleteContactInfoResponse.IsSuccess)
            {
                deleteContactInfoResponse.Data = new ContactInfo()
                {
                    UUID = result?.Entity.UUID ?? -1,
                    Contact = result?.Entity.Contact ?? new(),
                    InfoType = result?.Entity.InfoType ?? new(),
                    Info = result?.Entity.Info ?? string.Empty
                };
            }

            return deleteContactInfoResponse;
        }
    }
}
