﻿using Contacts.Application.Interfaces.Repositories;
using Contacts.Domain.Common;
using Contacts.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Persistence.Repository
{
    public class Repository<T> : IRepository<T> where T : class, IBaseEntity, new()
    {
        private readonly ApplicationDbContext _context;
        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        private DbSet<T> Table { get => _context.Set<T>(); }

        public virtual async Task<T> Add(T entity)
        {
            T result = Table.AddAsync(entity).Result.Entity;
            var retVal = await _context.SaveChangesAsync();
            return result;
        }

        public virtual async Task<T> Update(T entity)
        {
            T result = Table.Update(entity).Entity;
            await _context.SaveChangesAsync();
            return result;
        }

        public virtual async Task<T> Delete(T entity)
        {
            T result = Table.Remove(entity).Entity;
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<List<T>> GetAsync() => await Table.ToListAsync();

        public async Task<T> GetByIdAsync(int id) => await Table.FindAsync(id);

        public async Task<int> SaveChanges()
        { 
            return await _context.SaveChangesAsync(); 
        }
    }
}
