using CarRental.Application.Contracts.Persistence.IRepositories;
using CarRental.Domain.Common;
using CarRental.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CarRental.Persistence.Repositories;
public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : EntityBase
{
    private readonly ApplicationDbContext _dbContext;
    internal DbSet<TEntity> dbSet;

    public GenericRepository(ApplicationDbContext context)
    {
        _dbContext = context;
        this.dbSet = _dbContext.Set<TEntity>();
    }

    public virtual async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await dbSet.AddAsync(entity, cancellationToken);
        return entity;
    }

    public async Task<IEnumerable<TEntity>> FindAllAsync(CancellationToken cancellationToken)
    {
        IQueryable<TEntity> query = dbSet;

        foreach (var item in GetIncludeablePropertyNames())
        {
            query = query.Include(item);
        }

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
    {
        return await dbSet.Where(predicate).ToListAsync(cancellationToken);
    }

    public async Task<TEntity> FindSingleAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
    {
        return await dbSet.SingleOrDefaultAsync(predicate, cancellationToken);
    }

    public void Remove(TEntity entity)
    {
        dbSet.Remove(entity);
    }

    private static ICollection<string> GetIncludeablePropertyNames()
    {
        var entityType = typeof(TEntity);
        var properties = entityType.GetProperties()
            .Where(p => p.PropertyType.IsClass && !p.PropertyType.FullName!.StartsWith("System.")
                     && !p.PropertyType.IsArray && typeof(EntityBase).IsAssignableFrom(p.PropertyType))
            .Select(p => p.Name)
            .ToList();

        return properties;
    }
}
