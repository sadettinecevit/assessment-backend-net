using Contacts.Application.Dto;
using Contacts.Application.Dto.Command;
using Contacts.Application.Interfaces.UnitOfWork;
using Contacts.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace Contacts.Persistence.CQRS.Handlers.Command
{
    public class CreateInfoTypeCommandHandler : IRequestHandler<CreateInfoTypeDto, HandlerResponse<InfoType>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateInfoTypeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<HandlerResponse<InfoType>> Handle(CreateInfoTypeDto createContactInfoDto, CancellationToken cancellationToken)
        {
            HandlerResponse<InfoType> createInfoTypeResponse = new HandlerResponse<InfoType>();
            EntityEntry<InfoType> result = null;
            using IDbContextTransaction retVal = await _unitOfWork.BeginTansactionAsync();
            try
            {
                result = await _unitOfWork.InfoTypeRepository.Add(
                    new InfoType
                    {
                        Name = createContactInfoDto.Name
                    });
                createInfoTypeResponse.IsSuccess = retVal.CommitAsync().IsCompletedSuccessfully;
            }
            catch (Exception ex)
            {
                await retVal.RollbackAsync();
            }

            createInfoTypeResponse.Message = createInfoTypeResponse.IsSuccess ? "Success" : "Unsuccess";

            createInfoTypeResponse.Data = new InfoType()
            {
                UUID = result?.Entity.UUID ?? -1,
                Name = result?.Entity.Name ?? string.Empty
            };

            return createInfoTypeResponse;
        }
    }
}
