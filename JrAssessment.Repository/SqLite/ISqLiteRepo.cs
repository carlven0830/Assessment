using JrAssessment.Model.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JrAssessment.Repository.SqLite
{
    public interface ISqLiteRepo<T> where T : Entity
    {
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

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
    }
}
