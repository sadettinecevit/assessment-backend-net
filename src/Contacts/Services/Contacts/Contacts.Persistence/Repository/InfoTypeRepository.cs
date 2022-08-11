using Contacts.Application.Interfaces.Repositories;
using Contacts.Domain.Entities;
using Contacts.Persistence.Context;

namespace Contacts.Persistence.Repository
{
    public class InfoTypeRepository : Repository<InfoType>, IInfoTypeRepository
    {
        public InfoTypeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
