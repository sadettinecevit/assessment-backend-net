using Contacts.Application.Dto;
using Contacts.Application.Dto.Command;
using Contacts.Application.Interfaces.Repositories;
using Contacts.Domain.Entities;
using MediatR;

namespace Contacts.Persistence.CQRS.Handlers.Command
{
    public class CreateInfoTypeHandler : IRequestHandler<CreateInfoTypeDto, HandlerResponse<InfoType>>
    {
        private readonly IInfoTypeRepository _repository;
        public CreateInfoTypeHandler(IInfoTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<HandlerResponse<InfoType>> Handle(CreateInfoTypeDto createContactInfoDto, CancellationToken cancellationToken)
        {
            HandlerResponse<InfoType> createInfoTypeResponse = new HandlerResponse<InfoType>();
            InfoType result = null;

            try
            {
                result = await _repository.Add(
                    new InfoType
                    {
                        Name = createContactInfoDto.Name
                    });
                await _repository.SaveChanges();

                createInfoTypeResponse.IsSuccess = result != null;
            }
            catch (Exception ex)
            {
                createInfoTypeResponse.Message = ex.Message;
            }

            createInfoTypeResponse.Message = String.IsNullOrWhiteSpace(createInfoTypeResponse.Message)
                && createInfoTypeResponse.IsSuccess ? "Success" : "Unsuccess";

            createInfoTypeResponse.Data = new InfoType()
            {
                UUID = result?.UUID ?? -1,
                Name = result?.Name ?? string.Empty
            };

            return createInfoTypeResponse;
        }
    }
}
