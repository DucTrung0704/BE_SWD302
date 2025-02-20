using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private DbSet<TEntity> _dbSet;

        public GenericRepository(NuringServiceDbContext context)
        {
            _dbSet = context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public Task<List<TEntity>> GetAllAsync()
        {
            return _dbSet.ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(Guid? id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }
    }
}
