using Contacts.Application.Dto;
using Contacts.Application.Dto.Command;
using Contacts.Application.Interfaces.Repositories;
using Contacts.Domain.Entities;
using MediatR;

namespace Contacts.Persistence.CQRS.Handlers.Command
{
    public class UpdateContactInfoHandler : IRequestHandler<UpdateContactInfoDto, HandlerResponse<ContactInfo>>
    {
        private readonly IContactInfoRepository _repository;
        public UpdateContactInfoHandler(IContactInfoRepository repository)
        {
            _repository = repository;
        }

        public async Task<HandlerResponse<ContactInfo>> Handle(UpdateContactInfoDto updateContactInfoDto, CancellationToken cancellationToken)
        {
            HandlerResponse<ContactInfo> updateContactInfoResponse = new HandlerResponse<ContactInfo>();
            ContactInfo result = null;

            try
            {
                if (_repository.GetByIdAsync(updateContactInfoDto.UUID).Result != null)
                {
                    result = await _repository.Update(
                        new ContactInfo
                        {
                            UUID = updateContactInfoDto.UUID,
                            ContactId = updateContactInfoDto.ContactId, //_unitOfWork.ContactRepository.GetByIdAsync(updateContactInfoDto.ContactId).Result,
                            InfoTypeId = updateContactInfoDto.InfoTypeId, //_unitOfWork.InfoTypeRepository.GetByIdAsync(updateContactInfoDto.InfoTypeId).Result,
                            Info = updateContactInfoDto.Info
                        });
                    int changedItemCount = await _repository.SaveChanges();

                    updateContactInfoResponse.IsSuccess = changedItemCount > 0;
                }
            }
            catch (Exception ex)
            {
                updateContactInfoResponse.Message = ex.Message;
            }

            updateContactInfoResponse.Message = string.IsNullOrWhiteSpace(updateContactInfoResponse.Message) 
                && updateContactInfoResponse.IsSuccess ? "Success" : "Unsuccess";

            updateContactInfoResponse.Data = new ContactInfo()
            {
                UUID = result?.UUID ?? -1,
                ContactId = result?.ContactId ?? -1, //new(),
                InfoTypeId = result?.InfoTypeId ?? -1, //new(),
                Info = result?.Info ?? string.Empty
            };

            return updateContactInfoResponse;
        }
    }
}
