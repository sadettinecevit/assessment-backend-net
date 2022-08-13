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
    public class DeleteInfoTypeHandler : IRequestHandler<DeleteInfoTypeDto, HandlerResponse<InfoType>>
    {
        private readonly IInfoTypeRepository _repository;
        public DeleteInfoTypeHandler(IInfoTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<HandlerResponse<InfoType>> Handle(DeleteInfoTypeDto deleteInfoTypeDto, CancellationToken cancellationToken)
        {
            HandlerResponse<InfoType> deleteInfoTypeResponse = new HandlerResponse<InfoType>();
            InfoType result = null;

            try
            {
                InfoType obj = await _repository.GetByIdAsync(deleteInfoTypeDto.UUID);
                if (obj != null)
                {
                    result = await _repository.Delete(obj);
                    int changedItemCount = await _repository.SaveChanges();

                    deleteInfoTypeResponse.IsSuccess = changedItemCount > 0;
                }
            }
            catch (Exception ex)
            {
                deleteInfoTypeResponse.Message = ex.Message;
            }

            deleteInfoTypeResponse.Message = string.IsNullOrWhiteSpace(deleteInfoTypeResponse.Message) 
                && deleteInfoTypeResponse.IsSuccess ? "Success" : "Unsuccess";

            if (deleteInfoTypeResponse.IsSuccess)
            {
                deleteInfoTypeResponse.Data = new InfoType()
                {
                    UUID = result?.UUID ?? -1,
                    Name = result?.Name ?? string.Empty
                };
            }

            return deleteInfoTypeResponse;
        }
    }
}
