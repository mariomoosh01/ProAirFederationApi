using System.Linq.Expressions;

namespace ProAirApiServices.DataLayer.DataAccess.Repositories
{
    internal interface IRepository<T> where T : class
    {        
        List<T> GetAll();
        List<T> GetAll(Expression<Func<T, bool>> predicate);
        T? FirstOrDefault(Expression<Func<T, bool>> predicate);
        T? Add(T entity);
        List<T>? AddRange(List<T> entities);

    }
}
