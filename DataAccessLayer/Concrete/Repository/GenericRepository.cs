using DataAccessLayer.Abstract;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DataAccessLayer.Concrete.Repository
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly Context _context;
        private readonly DbSet<T> _object;

        public GenericRepository(Context context)
        {
            _context = context;
            _object = _context.Set<T>();
        }

        public async Task DeleteAsync(T p)
        {
            var deletedEntity = _context.Entry(p);
            deletedEntity.State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
        {
            return await _object.SingleOrDefaultAsync(filter);
        }

        public async Task InsertAsync(T p)
        {
            var addedEntity = _context.Entry(p);
            addedEntity.State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        public async Task<List<T>> ListAsync()
        {
            return await _object.ToListAsync();
        }

        public async Task<List<T>> ListAsync(Expression<Func<T, bool>> filter)
        {
            return await _object.Where(filter).ToListAsync();
        }

        public async Task UpdateAsync(T p)
        {
            var modifiedEntity = _context.Entry(p);
            modifiedEntity.State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public void Delete(T p)
        {
            var deletedEntity = _context.Entry(p);
            deletedEntity.State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            return _object.SingleOrDefault(filter);
        }

        public void Insert(T p)
        {
            var addedEntity = _context.Entry(p);
            addedEntity.State = EntityState.Added;
            _context.SaveChanges();
        }

        public List<T> List()
        {
            return _object.ToList();
        }

        public List<T> List(Expression<Func<T, bool>> filter)
        {
            return _object.Where(filter).ToList();
        }

        public void Update(T p)
        {
            var modifiedEntity = _context.Entry(p);
            modifiedEntity.State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
