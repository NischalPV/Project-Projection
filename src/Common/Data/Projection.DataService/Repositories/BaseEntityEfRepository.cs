using Microsoft.Extensions.Logging;
using Projection.Common.Specifications;
using Projection.DataService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Projection.DataService.Repositories;

public class BaseEntityEfRepository<TEntity, TKey, TContext> : IBaseEntityAsyncRepository<TEntity, TKey, TContext> where TEntity : BaseEntity<TKey> where TContext : BaseDbContext
{
    #region properties
    protected readonly TContext _ctx;
    private readonly ILogger<BaseEntityEfRepository<TEntity, TKey, TContext>> _logger;
    #endregion

    #region ctor
    public BaseEntityEfRepository(TContext ctx, ILogger<BaseEntityEfRepository<TEntity, TKey, TContext>> logger)
    {
        _ctx = ctx ?? throw new ArgumentNullException(nameof(ctx));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    #endregion

    #region interface implementation
    public IUnitOfWork UnitOfWork { get { return _ctx; } }

    public async Task<TEntity> AddAsync(TEntity entity, bool doSave = true, CancellationToken cancellationToken = default)
    {
        if (doSave)
        {
            try
            {
                var result = await _ctx.Set<TEntity>().AddAsync(entity);
                entity = result.Entity;
                await _ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occured while adding entity: {typeof(TEntity).Name}");
            }
        }
        else
        {
                return _ctx.Set<TEntity>().Add(entity).Entity;
        }

        return entity;

    }

    public async Task<int> CountAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(specification).CountAsync();
    }

    public async Task DeleteAsync(TEntity entity, bool doSave = true, CancellationToken cancellationToken = default)
    {
        entity.IsActive = false;
        await UpdateAsync(entity, doSave);
    }

    public async Task DeleteByIdAsync(TKey id, bool doSave = true, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id);
        await DeleteAsync(entity, doSave);
    }

    public async Task<TEntity> GetByIdAsync(TKey id, CancellationToken cancellationToken = default)
    {
        return await _ctx.Set<TEntity>().FindAsync(id);
    }

    public async Task<TEntity> GetByIdAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(specification).FirstOrDefaultAsync();
    }

    public async Task<bool> IsExists(TEntity entity, Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _ctx.Set<TEntity>().AnyAsync(predicate);
    }

    public async Task<List<TEntity>> ListAllAsync(CancellationToken cancellationToken = default)
    {
        return await _ctx.Set<TEntity>().Where(e => e.IsActive).ToListAsync();
    }

    public async Task<List<TEntity>> ListAllAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(specification).ToListAsync();
    }

    public async Task<TEntity> UpdateAsync(TEntity entity, bool doSave = true, CancellationToken cancellationToken = default)
    {
        if (doSave)
        {
            try
            {
                var result = _ctx.Entry<TEntity>(entity);
                result.State = EntityState.Modified;
                entity = result.Entity;
                await _ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occured while updating entity: {typeof(TEntity).Name}");
            }
        }
        else
        {
            return _ctx.Set<TEntity>().Update(entity).Entity;
        }

        return entity;
    }

    #endregion

    private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec)
    {
        return SpecificationEvaluator<TEntity, TKey>.GetQuery(_ctx.Set<TEntity>().AsQueryable(), spec);
    }
}
