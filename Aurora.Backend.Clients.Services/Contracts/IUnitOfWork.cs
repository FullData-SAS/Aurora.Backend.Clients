namespace Aurora.Backend.Clients.Services.Contracts;

public interface IUnitOfWork
{
    IRepository<T> GetRepository<T>() where T : class;
    Task<bool> CommitAsync();
}