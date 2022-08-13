using Contacts.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Application.Interfaces.UnitOfWork
{
    public interface IUnitOfWorkService : IAsyncDisposable
    {
        Task<IDbContextTransaction> BeginTansactionAsync();
        public IContactRepository ContactRepository { get; }
        public IContactInfoRepository ContactInfoRepository { get; }
        public IInfoTypeRepository InfoTypeRepository { get; }

    }
}
