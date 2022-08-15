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
    public class DeleteContactInfoHandler : IRequestHandler<DeleteContactInfoDto, HandlerResponse<ContactInfo>>
    {
        private readonly IContactInfoRepository _repository;
        public DeleteContactInfoHandler(IContactInfoRepository repository)
        {
            _repository = repository;
        }

        public async Task<HandlerResponse<ContactInfo>> Handle(DeleteContactInfoDto deleteContactInfoDto, CancellationToken cancellationToken)
        {
            HandlerResponse<ContactInfo> deleteContactInfoResponse = new HandlerResponse<ContactInfo>();
            ContactInfo result = null;

            try
            {
                ContactInfo obj = await _repository.GetByIdAsync(deleteContactInfoDto.UUID);
                if (obj != null)
                {
                    result = await _repository.Delete(obj);
                    await _repository.SaveChanges();

                    deleteContactInfoResponse.IsSuccess = result != null;
                }
            }
            catch (Exception ex)
            {
                deleteContactInfoResponse.Message = ex.Message;
            }

            deleteContactInfoResponse.Message = string.IsNullOrWhiteSpace(deleteContactInfoResponse.Message) 
                && deleteContactInfoResponse.IsSuccess ? "Success" : "Unsuccess";

            if (deleteContactInfoResponse.IsSuccess)
            {
                deleteContactInfoResponse.Data = new ContactInfo()
                {
                    UUID = result?.UUID ?? -1,
                    ContactId = result?.ContactId ?? -1, // new(),
                    InfoTypeId = result?.InfoTypeId ?? -1, //new(),
                    Info = result?.Info ?? string.Empty
                };
            }

            return deleteContactInfoResponse;
        }
    }
}
