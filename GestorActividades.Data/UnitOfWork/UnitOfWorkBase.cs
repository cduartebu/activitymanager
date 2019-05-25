using GestorActividades.Data.Repositories;
using System;
using System.Data;
using System.Data.Entity;

namespace GestorActividades.Data.UnitOfWork
{
    public class UnitOfWorkBase : IUnitOfWork
    {
        private DbContextTransaction myDbContextTransaction;        

        protected virtual DbContext DbContext { get; set; }

        private bool myDisposed;

        public UnitOfWorkBase(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        public IGenericRepository<TEntity> GetGenericRepository<TEntity>() where TEntity : class
        {
            return new GenericRepository<TEntity>(DbContext);
        }

        public void BeginTransaction()
        {
            myDbContextTransaction = DbContext.Database.BeginTransaction();
        }

        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            myDbContextTransaction = DbContext.Database.BeginTransaction(isolationLevel);
        }

        public void Commit()
        {
            myDbContextTransaction.Commit();
        }

        protected void Dispose(bool disposing)
        {
            if (!myDisposed && disposing)
            {
                DbContext.Dispose();
                myDbContextTransaction?.Dispose();
            }
            myDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void RollBack()
        {
            myDbContextTransaction.Rollback();
        }

        public int Save()
        {
            return DbContext.SaveChanges();
        }
    }
}
