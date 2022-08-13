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
    public class CreateContactInfoHandler : IRequestHandler<CreateContactInfoDto, HandlerResponse<ContactInfo>>
    {
        private readonly IContactInfoRepository _repository;
        public CreateContactInfoHandler(IContactInfoRepository repository)
        {
            _repository = repository;
        }

        public async Task<HandlerResponse<ContactInfo>> Handle(CreateContactInfoDto createContactInfoDto, CancellationToken cancellationToken)
        {
            HandlerResponse<ContactInfo> createContactInfoResponse = new HandlerResponse<ContactInfo>();
            ContactInfo result = null;

            try
            {
                //Contact contact = _unitOfWork.ContactRepository.GetByIdAsync(createContactInfoDto.ContactId).Result;
                //InfoType infoType = _unitOfWork.InfoTypeRepository.GetByIdAsync(createContactInfoDto.InfoTypeId).Result;
                result = await _repository.Add(
                    new ContactInfo
                    {
                        ContactId = createContactInfoDto.ContactId, //_unitOfWork.ContactRepository.GetByIdAsync(createContactInfoDto.ContactId).Result,
                        InfoTypeId = createContactInfoDto.InfoTypeId, //_unitOfWork.InfoTypeRepository.GetByIdAsync(createContactInfoDto.InfoTypeId).Result,
                        Info = createContactInfoDto.Info
                    });

                int changedItemCount = await _repository.SaveChanges();

                createContactInfoResponse.IsSuccess = changedItemCount > 0;
            }
            catch (Exception ex)
            {
                createContactInfoResponse.Message = ex.Message;
            }

            createContactInfoResponse.Message = createContactInfoResponse.IsSuccess ? "Success" : "Unsuccess";

            createContactInfoResponse.Data = new ContactInfo()
            {
                UUID = result?.UUID ?? -1,
                ContactId = result?.ContactId ?? -1, //new(),
                InfoTypeId = result?.InfoTypeId ?? -1, // new(),
                Info = result?.Info ?? string.Empty
            };

            return createContactInfoResponse;
        }
    }
}
