using Contacts.Domain.Common;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Application.Interfaces.Repositories
{
    public interface IRepository<T> where T : class, IBaseEntity, new()
    {
        Task<List<T>> GetAsync();
        Task<T> GetByIdAsync(int id);
        Task<EntityEntry<T>> Add(T entity);
        Task<EntityEntry<T>> Update(T entity);
        Task<EntityEntry<T>> Delete(T entity);
    }
}
