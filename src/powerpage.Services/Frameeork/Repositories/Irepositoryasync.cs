using System.Threading;
using System.Threading.Tasks;
using powerpage.Entities;
using powerpage.Services;

namespace powerpage.Services;

public interface IRepositoryAsync<TEntity> : IRepository<TEntity> where TEntity : class, IObjectState
{
    new Task<TEntity> FindAsync(params object[] keyValues);
    new Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues);
    new Task<bool> DeleteAsync(params object[] keyValues);
    new Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues);
}