using GestorActividades.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace GestorActividades.Data.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {       
        private readonly DbContext myDbContext;

        protected DbSet<TEntity> myDbSet;        

        /// <summary>
        /// Gets or sets the command timeout.
        /// </summary>
        /// <value>
        /// The command timeout.
        /// </value>
        public int? CommandTimeout
        {
            get { return myDbContext.Database.CommandTimeout; }
            set { myDbContext.Database.CommandTimeout = value; }
        }

        public GenericRepository(DbContext dbContext)
        {
            myDbContext = dbContext;
            myDbSet = myDbContext.Set<TEntity>();
        }

        public void Delete(TEntity item)
        {
            if (myDbContext.Entry(item).State == EntityState.Detached)
            {
                myDbSet.Attach(item);
            }
            myDbSet.Remove(item);
        }

        public void DeleteRange(Expression<Func<TEntity, bool>> predicate)
        {
            myDbSet.RemoveRange(myDbSet.Where(predicate));
        }

        public IEnumerable<T> ExecuteCustomStoredProc<T>(string commandName, params object[] parameters)
        {
            return (myDbContext as IObjectContextAdapter).ObjectContext.ExecuteStoreQuery<T>(commandName, parameters);
        }

        public TEntity Get(int id)
        {
            return myDbSet.Find(id);
        }

        public IQueryable<TEntity> GetAll()
        {
            return myDbSet;
        }

        public IQueryable<T> GetRepository<T>() where T : class
        {
            return myDbContext.Set<T>();
        }

        public IQueryable<TEntity> GetWithRawSql(string query, params object[] parameters)
        {
            return myDbContext.Set<TEntity>().SqlQuery(query, parameters).AsQueryable();
        }

        public int ExecuteSqlCommand(string query, params object[] parameters)
        {
            return myDbContext.Database.ExecuteSqlCommand(query, parameters);
        }

        public void Insert(TEntity item)
        {
            myDbSet.Add(item);
        }

        public void InsertRange(IEnumerable<TEntity> item)
        {
            myDbSet.AddRange(item);
        }

        public void InsertAndSave(TEntity item)
        {
            Insert(item);
            Save();
        }

        public void Update(TEntity item)
        {
            myDbSet.Attach(item);
            myDbContext.Entry(item).State = EntityState.Modified;
        }

        public void UpdateAndSave(TEntity item)
        {
            Update(item);
            Save();
        }

        public void Update(TEntity current, TEntity modified)
        {
            myDbContext.Entry(current).CurrentValues.SetValues(modified);
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return myDbSet.Where(predicate);
        }

        public int Save()
        {
            return myDbContext.SaveChanges();
        }
    }
}
