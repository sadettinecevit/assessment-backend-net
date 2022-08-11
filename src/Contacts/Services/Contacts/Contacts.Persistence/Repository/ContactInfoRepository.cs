using Contacts.Application.Interfaces.Repositories;
using Contacts.Domain.Entities;
using Contacts.Persistence.Context;

namespace Contacts.Persistence.Repository
{
    public class ContactInfoRepository : Repository<ContactInfo>, IContactInfoRepository
    {
        public ContactInfoRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
