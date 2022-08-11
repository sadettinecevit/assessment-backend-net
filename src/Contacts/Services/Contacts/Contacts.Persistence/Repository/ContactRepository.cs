using Contacts.Application.Interfaces.Repositories;
using Contacts.Domain.Entities;
using Contacts.Persistence.Context;

namespace Contacts.Persistence.Repository
{
    public class ContactRepository : Repository<Contact>, IContactRepository
    {
        public ContactRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
