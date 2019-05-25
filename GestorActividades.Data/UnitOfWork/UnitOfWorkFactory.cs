using System;
using System.Data.Entity;

namespace GestorActividades.Data.UnitOfWork
{
    public class UnitOfWorkFactory<TContext> : IUnitOfWorkFactory where TContext : DbContext, new()
    {
        public IUnitOfWork GetUnitOfWork()
        {
            return new UnitOfWorkBase(new TContext());
        }

        public IUnitOfWork GetUnitOfWork(string connectionString)
        {
            try
            {
                var instance = Activator.CreateInstance(typeof(TContext), new[] { connectionString }) as TContext;
                return new UnitOfWorkBase(instance);
            }
            catch (MissingMethodException)
            {
                return new UnitOfWorkBase(new TContext());
            }
        }
    }
}
