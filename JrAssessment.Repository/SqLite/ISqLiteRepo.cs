using JrAssessment.Model.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace JrAssessment.Repository.SqLite
{
    public interface ISqLiteRepo<T> where T : Entity
    {
        Task<T?> GetAsync(Expression<Func<T, bool>> expression, Func<IQueryable<T>, IQueryable<T>>? include = null);
        Task<(List<T> Content, long TotalCount)> GetAllAsync(List<Expression<Func<T, bool>>>? listFilter = null, Expression<Func<T, object>>? orderBy = null, bool asc = true, Func<IQueryable<T>, IQueryable<T>>? include = null);
        Task<(List<T> Content, long TotalCount)> GetAllByPaginationAsync(int pageNum, int pageSize, List<Expression<Func<T, bool>>>? listFilter = null, Expression<Func<T, object>>? orderBy = null, bool asc = true, Func<IQueryable<T>, IQueryable<T>>? include = null);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task<T?> DeleteAsync(Expression<Func<T, bool>> filter);
    }
    public class SqLiteRepo<T> : ISqLiteRepo<T> where T : Entity
    {
        private readonly SqLiteDbContext _context;
        private readonly DbSet<T> _dbSet;

        public SqLiteRepo(SqLiteDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T?> GetAsync(
            Expression<Func<T, bool>> filter,
            Func<IQueryable<T>, IQueryable<T>>? include = null
        )
        {
            IQueryable<T> query = _dbSet.Where(x => x.IsEnabled);

            if (include != null)
            {
                query = include(query);
            }

            return await query.FirstOrDefaultAsync(filter);
        }

        public async Task<(List<T> Content, long TotalCount)> GetAllAsync(
            List<Expression<Func<T, bool>>>? listFilter = null,
            Expression<Func<T, object>>? orderBy = null,
            bool asc = true,
            Func<IQueryable<T>, IQueryable<T>>? include = null
        )
        {
            IQueryable<T> query = _dbSet.Where(x => x.IsEnabled);

            if (include != null)
            {
                query = include(query);
            }

            if (listFilter != null)
            {
                foreach(var filter in listFilter)
                {
                    query = query.Where(filter);
                }
            }

            if (orderBy != null)
            {
                query = asc ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);
            }

            long totalCount = await query.LongCountAsync();

            var content = await query.ToListAsync();

            return (content, totalCount);
        }

        public async Task<(List<T> Content, long TotalCount)> GetAllByPaginationAsync(
            int pageNum,
            int pageSize,
            List<Expression<Func<T, bool>>>? listFilter = null,
            Expression<Func<T, object>>? orderBy = null,
            bool asc = true,
            Func<IQueryable<T>, IQueryable<T>>? include = null
        )
        {
            IQueryable<T> query = _dbSet.Where(x => x.IsEnabled);

            if (include != null)
            {
                query = include(query);
            }

            if (listFilter != null)
            {
                foreach(var filter in listFilter)
                {
                    query = query.Where(filter);
                }
            }

            if (orderBy != null)
            {
                query = asc ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);
            }

            long totalCount = await query.LongCountAsync();

            var content = await query.Skip((pageNum - 1) * pageSize).Take(pageSize).ToListAsync();

            return (content, totalCount);
        }

        public async Task AddAsync(T entity)
        {
            entity.Id         = Guid.NewGuid();
            entity.CreateDate = DateTime.UtcNow;

            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            entity.ModifiedDate = DateTime.UtcNow;

            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T?> DeleteAsync(Expression<Func<T, bool>> filter)
        {
            var entity = await _dbSet.Where(filter).FirstOrDefaultAsync();

            if (entity != null)
            {
                entity.IsEnabled = false;

                _dbSet.Update(entity);

                await _context.SaveChangesAsync();
            }

            return entity;
        }
    }
}
