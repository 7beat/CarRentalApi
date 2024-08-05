using CarRental.Domain.Common;
using System.Linq.Expressions;

namespace CarRental.Application.Contracts.Persistence.IRepositories;
public interface IGenericRepository<TEntity> where TEntity : EntityBase
{
    Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken);
    Task<TEntity?> FindSingleAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
    Task<IEnumerable<TEntity>> FindAllAsync(CancellationToken cancellationToken);
    Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
    void Remove(TEntity entity);
}
