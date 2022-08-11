using Contacts.Application.Dto;
using Contacts.Application.Dto.Command;
using Contacts.Application.Interfaces.UnitOfWork;
using Contacts.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace Contacts.Persistence.CQRS.Handlers.Command
{
    public class UpdateInfoTypeCommandHandler : IRequestHandler<UpdateInfoTypeDto, HandlerResponse<InfoType>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateInfoTypeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<HandlerResponse<InfoType>> Handle(UpdateInfoTypeDto updateInfoTypeDto, CancellationToken cancellationToken)
        {
            HandlerResponse<InfoType> updateInfoTypeResponse = new HandlerResponse<InfoType>();
            EntityEntry<InfoType> result = null;
            using IDbContextTransaction retVal = await _unitOfWork.BeginTansactionAsync();
            try
            {
                if (_unitOfWork.InfoTypeRepository.GetByIdAsync(updateInfoTypeDto.UUID).Result != null)
                {
                    result = await _unitOfWork.InfoTypeRepository.Update(
                        new InfoType
                        {
                            UUID = updateInfoTypeDto.UUID,
                            Name = updateInfoTypeDto.Name
                        });
                    updateInfoTypeResponse.IsSuccess = retVal.CommitAsync().IsCompletedSuccessfully;
                }
            }
            catch (Exception ex)
            {
                await retVal.RollbackAsync();
            }

            updateInfoTypeResponse.Message = updateInfoTypeResponse.IsSuccess ? "Success" : "Unsuccess";

            updateInfoTypeResponse.Data = new InfoType()
            {
                Name = result?.Entity.Name ?? string.Empty
            };

            return updateInfoTypeResponse;
        }
    }
}
