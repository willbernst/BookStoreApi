using Microsoft.EntityFrameworkCore;
using Project.Business.Interfaces;
using Project.Business.Models;
using Project.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.Repository
{
    public abstract class Repository<T> : IGenericRepository<T> where T : Entity, new()
    {
        protected readonly MyDbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public Repository(MyDbContext db)
        {
            _dbContext = db;
            _dbSet = db.Set<T>();
        }

        public async Task<IEnumerable<T>> Search(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<T> GetById(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<List<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task Add(T entity)
        {
            _dbSet.Add(entity);
            await SaveChanges();
        }

        public async Task Update(T entity)
        {
            _dbSet.Update(entity);
            await SaveChanges();
        }


        public async Task Remove(Guid id)
        {
            _dbSet.Remove(new T { Id = id });
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}
