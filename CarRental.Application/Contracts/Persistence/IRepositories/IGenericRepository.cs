using System.Linq.Expressions;
using CarRental.Domain.Common;

namespace CarRental.Application.Contracts.Persistence.IRepositories;
public interface IGenericRepository<TEntity> where TEntity : EntityBase
{
    Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken);
    Task<TEntity?> FindSingleAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
    Task<TEntity?> FindSingleAsync(Expression<Func<TEntity, bool>> predicate, bool trackChanges = false, CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> FindAllAsync(CancellationToken cancellationToken);
    Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
    void Remove(TEntity entity);
}
