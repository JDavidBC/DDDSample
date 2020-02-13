using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Source.Pagination.Dto;
using Source.Pagination.Implementations;

namespace Source.Core.DataService
{
    public interface IEntityDataService<TEntity> where TEntity : class, new()
    {
        Task<TEntity> GetById<TId>(TId id);
        Task<TEntity> GetFirstOrDefault(Expression<Func<TEntity, bool>> criteria);
        Task<IList<TEntity>> GetMultiple(Expression<Func<TEntity, bool>> criteria);
        Task<IList<TEntity>> GetAll();
        Task<Envelope<IEnumerable<TEntity>>> GetPaginate(PaginationDto paginationDto);
        Task<IQueryable<TEntity>> Get(int page, int records, Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", bool asNoTracking = false);
        //Task<IQueryable<TEntity>> Get(int page, int records, Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", bool asNoTracking = false);
        Task<TEntity> Add(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task Delete(TEntity entity);
    }
}