namespace GestorActividades.Data.UnitOfWork
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork GetUnitOfWork();

        IUnitOfWork GetUnitOfWork(string connectionString);
    }
}
