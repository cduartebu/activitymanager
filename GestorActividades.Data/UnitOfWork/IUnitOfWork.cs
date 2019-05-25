using System;
using System.Data;

namespace GestorActividades.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> GetGenericRepository<TEntity>() where TEntity : class;

        void Commit();

        int Save();

        void BeginTransaction();

        void BeginTransaction(IsolationLevel isolationLevel);

        void RollBack();
    }
}
