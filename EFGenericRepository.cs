using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DAL.Interface;

namespace DAL.GenericRepository
{
    public abstract class EFGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity :class
    {
        private DbContext _context;
        private DbSet<TEntity> _dbSet;

        public EFGenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }
        
        public void Create(TEntity item)
        {
            _dbSet.Add(item);
            _context.SaveChanges();
        }
        public async Task CreateAsync(TEntity item)
        {
            _dbSet.Add(item);
            await _context.SaveChangesAsync();
        }

        public TEntity FindById(int? id)
        {
            var tempValue = _dbSet.Find(id);
            return tempValue;
        }
        public async Task<TEntity> FindByIdAsync(int? id)
        {
            var tempValue = await _dbSet.FindAsync(id);
            return tempValue;
        }

        public IEnumerable<TEntity> Get()
        {
            var tempValue = _dbSet.AsEnumerable();
            return tempValue;
        }
        public async Task<List<TEntity>> GetAsync()
        {
            var tempValue = await _dbSet.ToListAsync();
            return tempValue;
        }

        public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            var tempValue = _dbSet.Where(predicate);
            return tempValue;
        }
        public async Task<IEnumerable<TEntity>> GetAsync(Func<TEntity, bool> predicate)
        {
            var tempValue = await Task.Run(() => _dbSet.Where(predicate));
            return tempValue;
        }

        public void Remove(TEntity item)
        {
            TEntity findItem = _dbSet.Find(item);
            if (findItem != null)
            {
                _dbSet.Remove(findItem);
            }
        }
        public async Task RemoveAsync(TEntity item)
        {
            TEntity findItem = await _dbSet.FindAsync(item);
            if (findItem != null)
            {
                _dbSet.Remove(findItem);
            }
            await _context.SaveChangesAsync();
        }
        public void Update(TEntity item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public async Task UpdateAsync(TEntity item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
