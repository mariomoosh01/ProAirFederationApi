
using Azure;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Linq;
using System.Linq.Expressions;

namespace ProAirApiServices.DataLayer.DataAccess.Repositories
{
    internal class Repository<T> : IRepository<T> where T : class
    {
        #region Constants
        private const string TAG = "DbContext";
        #endregion
        #region Fields
        private static void LogError(string message) => Log.Error($"{TAG}: {message}");
        private readonly DbContext _db;
        private readonly DbSet<T> _table;
        
        #endregion

        public Repository(DbContext db)
        {
            _db = db;
            _table = _db.Set<T>();
        }

        public T? FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return _table.FirstOrDefault(predicate);
        }

        public List<T> GetAll()
        {
            return _table.ToList();
        }

        public List<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return _table.Where(predicate).ToList();
        }

        public T? Add(T entity)
        {
            try
            {
                _db.Add(entity);
                _db.SaveChanges();

                return entity;
            }
            catch (Exception ex)
            {
                LogError($"Error while commiting changes: {ex.Message}");
                return null;
            }
        }

        public List<T>? AddRange(List<T> entities)
        {
            try
            {
                _db.AddRange(entities);
                _db.SaveChanges();

                return entities;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
