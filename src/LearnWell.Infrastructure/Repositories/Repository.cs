using LearnWell.Application.Common.Interfaces;
using LearnWell.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LearnWell.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly LearnWellDbContext _db;
        internal DbSet<T> _dbSet;
        public Repository(LearnWellDbContext db)
        {
            _db = db;
            _dbSet = db.Set<T>();
        }
        public Task AddAsync(T entity)
        {
            //this is same as _db.entities.add();
            _dbSet.Add(entity);
            return Task.CompletedTask;
        }

        public Task Any(Expression<Func<T, bool>> filter)
        {
            _dbSet.Any(filter);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            return Task.CompletedTask;
        }
        
        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProps = null)
        {
            try
            {
                //same as IQueryable<SKU> query = _db.SKUs;
                IQueryable<T> query = _dbSet;
                if (filter != null)
                {
                    query = query.Where(filter);
                }
                if (!string.IsNullOrEmpty(includeProps))
                {
                    foreach (var includeProp in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProp);
                    }
                }
                return await query.ToListAsync();
            }
            catch (Exception e)
            {
                throw;
            }            
        }
        public async Task<T> GetAsync(Expression<Func<T, bool>> filter, string? includeProps = null)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrEmpty(includeProps))
            {
                foreach (var includeProp in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<T> GetLastAsync()
        {
            //use reflection
            var property = typeof(T).GetProperty("Id") ?? typeof(T).GetProperty("id");
            if (property == null)
            {
                throw new InvalidOperationException("Entity does not have an Id property.");
            }

            return await _dbSet.OrderByDescending(e => EF.Property<int>(e, property.Name))
                              .FirstOrDefaultAsync();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Attach(entity);
            _db.Entry(entity).State = EntityState.Modified;
            await Task.CompletedTask; // Actual DB save happens in UnitOfWork.SaveAsync()
            return entity;
        }
    }
}
