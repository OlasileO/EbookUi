using Ebook_Model;
using Ebook_Service.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ebook_Service.Implementation
{
    public class GenericRepository<T>: IGenericRepository<T> where T : class
    {
        private readonly EbookDbContext _dbContext;
        private DbSet<T> _entity = null;

        public GenericRepository(EbookDbContext dbContext)
        {
            _dbContext = dbContext;
          _entity = _dbContext.Set<T>();
        }

        public async Task<bool> AddAsync(T model)
        {
            try
            {
                _entity.Add(model);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        
        public async Task<bool> DeleteAsync(int id)
        {
           try
            {
                var result = await _entity.FindAsync(id);
                EntityEntry entityEntry = _dbContext.Entry<T>(result);
                entityEntry.State = EntityState.Deleted;
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return  await _entity.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] expressions)
        {
            IQueryable<T> query = _entity;
            query = expressions.Aggregate(query, (current, expressions) => current.Include(expressions));
            return await query.ToListAsync();
        }

        public T GetByIdAync(object id)
        {
            return  _entity.Find(id);
        }

        public async Task<bool> UpdateAsync(T model)
        {
            try
            {
                EntityEntry entityEntry = _entity.Entry(model);
                entityEntry.State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}
