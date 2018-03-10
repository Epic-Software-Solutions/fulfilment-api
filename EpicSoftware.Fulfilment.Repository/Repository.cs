using System.Collections.Generic;
using System.Threading.Tasks;
using EpicSoftware.Fulfilment.Context;
using EpicSoftware.Fulfilment.Domain;
using Microsoft.EntityFrameworkCore;

namespace EpicSoftware.Fulfilment.Repository
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly FulfilmentContext Context;
        protected readonly DbSet<T> DbSet;

        public Repository(FulfilmentContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }
        
        public Task<T> GetById(int id)
        {
            return DbSet.FindAsync(id);
        }

        public Task<List<T>> GetAll()
        {
            return DbSet.ToListAsync();
        }

        public async Task<T> Create(T entity)
        {
            DbSet.Add(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task Update(T entity)
        {
            
            DbSet.Update(entity);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);
            DbSet.Remove(entity);
            await Context.SaveChangesAsync();
        }
    }
}