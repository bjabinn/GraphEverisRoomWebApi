using ApiRooms.Graph.Entities;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;

namespace ApiRooms.Graph.Repositories
{
    public abstract class LinqBaseRepository<T, TContext> : IRepository<T>
        where T : BaseEntity
        where TContext : DbContext, new()
    {
        #region Fields
        private readonly TContext _context;
        #endregion

        #region Properties
        public TContext Context
        {
            get
            {
                return _context;
            }
        }
        #endregion

        #region Constructors
        protected LinqBaseRepository()
        {
            _context = new TContext();
        }
        #endregion

        #region IRepository Members
        public T GetOne(Expression<Func<T, bool>> expression)
        {
            return GetOne(expression, null);
        }

        public T GetOne(Expression<Func<T, bool>> expression, params string[] navigationLazyLoading)
        {
            var query = GetAll();
            if (navigationLazyLoading != null && navigationLazyLoading.Any())
            {
                foreach (var navigationProperty in navigationLazyLoading)
                {
                    query.Include(navigationProperty);
                }
            }
            return query.FirstOrDefault(expression);
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsQueryable();
        }

        public virtual IQueryable<T> GetByCriteria(Expression<Func<T, bool>> predicate)
        {
            return GetByCriteria(predicate, null);
        }

        public virtual IQueryable<T> GetByCriteria(Expression<Func<T, bool>> predicate, params string[] navigationLazyLoading)
        {
            var query = _context.Set<T>().Where<T>(predicate).AsQueryable<T>();

            if (navigationLazyLoading != null && navigationLazyLoading.Any())
            {
                foreach (var navigationProperty in navigationLazyLoading)
                {
                    query.Include(navigationProperty);
                }
            }
            return query;
        }

        public virtual IQueryable<T> GetByCriteria(Expression<Func<T, bool>> filter, out int total, int index = 0, int size = 50)
        {
            return GetByCriteria(filter, out total, index, size, null);
        }

        public virtual IQueryable<T> GetByCriteria(Expression<Func<T, bool>> filter, out int total, int index = 0, int size = 50, params string[] navigationLazyLoading)
        {
            var skipCount = index * size;
            var query = GetByCriteria(filter, navigationLazyLoading);

            query = skipCount == 0 ? query.Take(size) : query.Skip(skipCount).Take(size);

            total = query.Count();

            return query;
        }

        public IQueryable<T> GetByStepsOrdered(string orderBy, int index, int stepSize)
        {
            IQueryable<T> query;

            if (index == 0)
            {
                query = _context.Set<T>().OrderBy<T>(orderBy).Take(stepSize).AsQueryable<T>();
            }
            else
            {
                query = _context.Set<T>().OrderBy<T>(orderBy).Skip(index).Take(stepSize).AsQueryable<T>();
            }
            return query;
        }

        public int CountByCriteria(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().AsQueryable().Count(predicate);
        }

        public int CountAll()
        {
            return _context.Set<T>().AsQueryable().Count();
        }

        public virtual T Create(T TObject)
        {
            var newEntry = _context.Set<T>().Add(TObject);
            _context.SaveChanges();
            return newEntry;
        }

        public virtual int Delete(T TObject)
        {
            _context.Set<T>().Remove(TObject);
            return _context.SaveChanges();
        }

        public virtual int Update(T TObject)
        {
            try
            {
                Context.Set<T>().Attach(TObject);
                Context.Entry(TObject).State = EntityState.Modified;

                return _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual int Delete(Expression<Func<T, bool>> predicate)
        {
            var objects = GetByCriteria(predicate);
            foreach (var obj in objects)
            {
                _context.Set<T>().Remove(obj);
            }
            return _context.SaveChanges();
        }

        public bool Contains(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Count<T>(predicate) > 0;
        }

        public virtual T Find(params object[] keys)
        {
            return _context.Set<T>().Find(keys);
        }

        public virtual T Find(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefault<T>(predicate);
        }

        public virtual void ExecuteProcedure(String procedureCommand, params SqlParameter[] sqlParams)
        {
            _context.Database.ExecuteSqlCommand(procedureCommand, sqlParams);
        }



        public virtual T Reload(T entity, params string[] navigationLazyLoading)
        {
            EnsureEntityIsAttachedToContext(entity);
            _context.Entry(entity).Reload();

            if (navigationLazyLoading == null) return entity;
            foreach (var navigationProperty in navigationLazyLoading)
            {
                _context.Entry(entity).Reference(navigationProperty).Load();
            }

            return entity;
        }

        public virtual T ReloadColleccion(T entity, params string[] navigationLazyLoading)
        {
            Reload(entity);

            if (navigationLazyLoading == null) return entity;
            foreach (var navigationProperty in navigationLazyLoading)
            {
                _context.Entry(entity).Collection(navigationProperty).Load();
            }

            return entity;
        }


        public virtual int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public void CommitChanges()
        {
            _context.SaveChanges();
        }
        #endregion

        #region Private Methods
        private void EnsureEntityIsAttachedToContext(T entity)
        {

            var e = _context.Entry(entity);
            if (e.State == EntityState.Detached)
            {
                _context.Set<T>().Attach(entity);
            }
        }

        #endregion


    }
}