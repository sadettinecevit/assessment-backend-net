using Contacts.Application.Dto;
using Contacts.Application.Dto.Command;
using Contacts.Application.Interfaces.UnitOfWork;
using Contacts.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace Contacts.Persistence.CQRS.Handlers.Command
{
    public class DeleteInfoTypeCommandHandler : IRequestHandler<DeleteInfoTypeDto, HandlerResponse<InfoType>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteInfoTypeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<HandlerResponse<InfoType>> Handle(DeleteInfoTypeDto deleteInfoTypeDto, CancellationToken cancellationToken)
        {
            HandlerResponse<InfoType> deleteInfoTypeResponse = new HandlerResponse<InfoType>();
            EntityEntry<InfoType> result = null;

            using IDbContextTransaction retVal = await _unitOfWork.BeginTansactionAsync();
            try
            {
                InfoType obj = await _unitOfWork.InfoTypeRepository.GetByIdAsync(deleteInfoTypeDto.UUID);
                if (obj != null)
                {
                    result = await _unitOfWork.InfoTypeRepository.Delete(obj);
                    deleteInfoTypeResponse.IsSuccess = retVal.CommitAsync().IsCompletedSuccessfully;
                }
            }
            catch (Exception ex)
            {
                await retVal.RollbackAsync();
            }

            deleteInfoTypeResponse.Message = deleteInfoTypeResponse.IsSuccess ? "Success" : "Unsuccess";

            if (deleteInfoTypeResponse.IsSuccess)
            {
                deleteInfoTypeResponse.Data = new InfoType()
                {
                    UUID = result?.Entity.UUID ?? -1,
                    Name = result?.Entity.Name ?? string.Empty
                };
            }

            return deleteInfoTypeResponse;
        }
    }
}
