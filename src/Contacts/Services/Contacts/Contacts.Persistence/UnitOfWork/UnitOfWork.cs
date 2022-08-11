using Contacts.Application.Interfaces.Repositories;
using Contacts.Application.Interfaces.UnitOfWork;
using Contacts.Persistence.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace Contacts.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IContactRepository ContactRepository { get; set; }
        public IContactInfoRepository ContactInfoRepository { get; set; }
        public IInfoTypeRepository InfoTypeRepository { get; set; }

        public UnitOfWork(ApplicationDbContext context, IContactRepository contactRepository,
            IContactInfoRepository contactInfoRepository, IInfoTypeRepository infoTypeRepository)
        {
            _context = context;
            ContactRepository = contactRepository;
            ContactInfoRepository = contactInfoRepository;
            InfoTypeRepository = infoTypeRepository;
        }

        public async Task<IDbContextTransaction> BeginTansactionAsync() => await _context.Database.BeginTransactionAsync();

        public async ValueTask DisposeAsync() { }
    }
}