using Contacts.Application.Interfaces.UnitOfWork;

namespace Contacts.Persistence.CQRS.Handlers
{
    public abstract class BaseHandler
    {
        public readonly IUnitOfWork _unitOfWork;
        public BaseHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
