using Contacts.Application.Dto;
using Contacts.Application.Dto.Command;
using Contacts.Application.Interfaces.Repositories;
using Contacts.Domain.Entities;
using MediatR;

namespace Contacts.Persistence.CQRS.Handlers.Command
{
    public class UpdateInfoTypeHandler : IRequestHandler<UpdateInfoTypeDto, HandlerResponse<InfoType>>
    {
        private readonly IInfoTypeRepository _repository;
        public UpdateInfoTypeHandler(IInfoTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<HandlerResponse<InfoType>> Handle(UpdateInfoTypeDto updateInfoTypeDto, CancellationToken cancellationToken)
        {
            HandlerResponse<InfoType> updateInfoTypeResponse = new HandlerResponse<InfoType>();
            InfoType result = null;

            try
            {
                if (_repository.GetByIdAsync(updateInfoTypeDto.UUID).Result != null)
                {
                    result = await _repository.Update(
                        new InfoType
                        {
                            UUID = updateInfoTypeDto.UUID,
                            Name = updateInfoTypeDto.Name
                        });
                    await _repository.SaveChanges();

                    updateInfoTypeResponse.IsSuccess = result != null;
                }
            }
            catch (Exception ex)
            {
                updateInfoTypeResponse.Message = ex.Message;
            }

            updateInfoTypeResponse.Message = string.IsNullOrWhiteSpace(updateInfoTypeResponse.Message) 
                && updateInfoTypeResponse.IsSuccess ? "Success" : "Unsuccess";

            updateInfoTypeResponse.Data = new InfoType()
            {
                UUID = result?.UUID ?? -1,
                Name = result?.Name ?? string.Empty
            };

            return updateInfoTypeResponse;
        }
    }
}
