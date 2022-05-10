using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class GenericRepository : IGenericRepository
    {
        private readonly LabsDbContext _dbContext;
        public GenericRepository(LabsDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public void Add<T>(T entity) where T : class
        {
            _dbContext.Add(entity);
        }

        public IQueryable<T> GetAll<T>() where T : class
        {
            return _dbContext.Set<T>();
        }

        public void Update<T>(T entity) where T : class
        {
            _dbContext.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _dbContext.Remove(entity);
        }

        public T GetById<T>(Guid Id) where T : class
        {
            return _dbContext.Set<T>().Find(Id);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
