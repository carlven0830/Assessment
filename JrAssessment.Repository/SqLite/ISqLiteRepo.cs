using JrAssessment.Model.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JrAssessment.Repository.SqLite
{
    public interface ISqLiteRepo<T> where T : Entity
    {
        Task<T?> GetAsync(Expression<Func<T, bool>> expression);
        Task<T?> GetByOrderAsync(Expression<Func<T, object>> orderBy, bool asc = true);
        Task AddAsync(T entity);
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

        public async Task<T?> GetAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbSet.FirstOrDefaultAsync(filter);
        }

        public async Task<T?> GetByOrderAsync(
            Expression<Func<T, object>> orderBy,
            bool asc = true
        )
        {
            IQueryable<T> query = asc ? _dbSet.OrderBy(orderBy) : _dbSet.OrderByDescending(orderBy);

            return await query.FirstOrDefaultAsync();
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

        //public async Task DeleteAsync(Expression<Func<T, bool>> filter)
        //{
        //    var
        //}
    }
}
