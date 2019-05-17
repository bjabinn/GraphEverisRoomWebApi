using ApiRooms.Graph.Entities;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace ApiRooms.Graph.Repositories
{
    public interface IRepository<T>: IDisposable where T:BaseEntity
    {
        /// <summary>
        /// Gets all objects from database
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();
        /// <summary>
        /// Gets objects from database by filter.
        /// </summary>
        /// <param name="predicate">Specified a filter</param>
        /// <returns></returns>
        IQueryable<T> GetByCriteria(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Gets objects from database by filter.
        /// </summary>
        /// <param name="predicate">Specified a filter</param>
        /// <returns></returns>
        IQueryable<T> GetByCriteria(Expression<Func<T, bool>> predicate, params string[] navigationLazyLoading);

        /// <summary>
        /// Count by selected criteria
        /// </summary>
        /// <returns></returns>
        int CountByCriteria(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Count all Register
        /// </summary>
        /// <returns></returns>
        int CountAll();

        /// <summary>
        /// Gets objects from database with filting and paging.
        /// </summary>
        /// <typeparam name="Key"></typeparam>
        /// <param name="filter">Specified a filter</param>
        /// <param name="total">Returns the total records count of the filter.</param>
        /// <param name="index">Specified the page index.</param>
        /// <param name="size">Specified the page size</param>
        /// <returns></returns>
        IQueryable<T> GetByCriteria(Expression<Func<T, bool>> filter, out int total, int index = 0, int size = 50);

        /// <summary>
        /// Gets objects from database with filting and paging.
        /// </summary>
        /// <typeparam name="Key"></typeparam>
        /// <param name="filter">Specified a filter</param>
        /// <param name="total">Returns the total records count of the filter.</param>
        /// <param name="index">Specified the page index.</param>
        /// <param name="size">Specified the page size</param>
        /// <returns></returns>
        IQueryable<T> GetByCriteria(Expression<Func<T, bool>> filter, out int total, int index = 0, int size = 50, params string[] navigationLazyLoading);

        /// <summary>
        /// Gets objects from database given an index and an stepsize.
        /// </summary>
        /// <param name="orderBy">Column which will be used for ordering the table.</param>
        /// <param name="index">Specified the page index.</param>
        /// <param name="stepSize">Specified the page size</param>
        /// <returns></returns>
        IQueryable<T> GetByStepsOrdered(string orderBy, int index, int stepSize);

        /// <summary>
        /// Gets the object(s) is exists in database by specified filter.
        /// </summary>
        /// <param name="predicate">Specified the filter expression</param>
        /// <returns></returns>
        bool Contains(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Find object by keys.
        /// </summary>
        /// <param name="keys">Specified the search keys.</param>
        /// <returns></returns>
        T Find(params object[] keys);

        /// <summary>
        /// Find object by specified expression.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        T Find(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Create a new object to database.
        /// </summary>
        /// <param name="t">Specified a new object to create.</param>
        /// <returns></returns>
        T Create(T t);

        /// <summary>
        /// Delete the object from database.
        /// </summary>
        /// <param name="t">Specified a existing object to delete.</param>
        int Delete(T t);

        /// <summary>
        /// Delete objects from database by specified filter expression.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int Delete(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Update object changes and save to database.
        /// </summary>
        /// <param name="t">Specified the object to save.</param>
        /// <returns></returns>
        int Update(T t);

        /// <summary>
        /// Select Single Item by specified expression.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        T GetOne(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Select Single Item by specified expression.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        T GetOne(Expression<Func<T, bool>> expression, params string[] navigationLazyLoading);

        T Reload(T entity, params string[] navigationLazyLoading);

        int SaveChanges();

        void ExecuteProcedure(String procedureCommand, params SqlParameter[] sqlParams);
    }
}
