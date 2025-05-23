using Aurora.Backend.Clients.Services.Contracts;
using Aurora.Backend.Clients.Services.Persistence.Context;

namespace Aurora.Backend.Clients.Services.Implements;

public class UnitOfWork : IUnitOfWork
{
    private readonly AuroraContext _context;
    private bool _disposed;
    private Dictionary<Type, object> _repositories;

    public UnitOfWork(AuroraContext context)
    {
        _context = context;
        _repositories = new Dictionary<Type, object>();
    }

    public IRepository<T> GetRepository<T>() where T : class
    {
        if (_repositories.ContainsKey(typeof(T)))
        {
            return (IRepository<T>)_repositories[typeof(T)];
        }

        var repository = new Repository<T>(_context);
        _repositories.Add(typeof(T), repository);
        return repository;
    }

    public async Task<bool> CommitAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}