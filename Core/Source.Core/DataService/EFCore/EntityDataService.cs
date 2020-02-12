using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Source.Pagination.Dto;
using Source.Pagination.Implementations;
using Source.Pagination.Interfaces;

namespace Source.Core.DataService.EFCore
{
    public class EntityDataService<TEntity> : IEntityDataService<TEntity> where TEntity : class, new()
    {
        protected readonly DbContext DbContext;
        private DbSet<TEntity> _dbSet;
        private readonly IPageHelper _pageHelper;

        public EntityDataService(DbContext dbContext, IPageHelper pageHelper)
        {
            DbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
            _pageHelper = pageHelper;            
        }

        public EntityDataService()
        {
        }

        public virtual async Task<TEntity> GetById<TId>(TId id)
        {
            return await DbContext.Set<TEntity>().FindAsync(id);
        }
      
        public virtual async Task<TEntity> Get<TId>(TEntity entityId, string includeProperties = "", string includeCollections = "")
        {
            //Si no hay propiedades ni colecciones a incluir
            if (String.IsNullOrWhiteSpace(includeProperties) && String.IsNullOrWhiteSpace(includeCollections))
                return await _dbSet.FindAsync(entityId);

            var entity = await _dbSet.FindAsync(entityId);

            if (entity == null)
                throw new ArgumentNullException("entity", "Entity not found");

            //Include properties
            if (!String.IsNullOrWhiteSpace(includeProperties))
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    await DbContext.Entry(entity).Reference(includeProperty.Trim()).LoadAsync();

            //Include collections
            if (!String.IsNullOrWhiteSpace(includeCollections))
                foreach (var includeCollection in includeCollections.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    await DbContext.Entry(entity).Collection(includeCollection.Trim()).LoadAsync();

            return entity;
        }

        public virtual async Task<TEntity> Get<TId>(TId entityId, string includeProperties = "", string includeCollections = "")
        {
            //Si no hay propiedades ni colecciones a incluir
            if (String.IsNullOrWhiteSpace(includeProperties) && String.IsNullOrWhiteSpace(includeCollections))
                return await _dbSet.FindAsync(entityId);

            var entity = await _dbSet.FindAsync(entityId);

            if (entity == null)
                throw new ArgumentNullException("entity", "Entity not found");

            //Include properties
            if (!String.IsNullOrWhiteSpace(includeProperties))
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    await DbContext.Entry(entity).Reference(includeProperty.Trim()).LoadAsync();

            //Include collections
            if (!String.IsNullOrWhiteSpace(includeCollections))
                foreach (var includeCollection in includeCollections.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    await DbContext.Entry(entity).Collection(includeCollection.Trim()).LoadAsync();

            return entity;
        }

         public virtual async Task<IQueryable<TEntity>> Get(int page, int records, Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", bool asNoTracking = false)
        {
            IQueryable<TEntity> query = asNoTracking ? _dbSet.AsNoTracking<TEntity>() : _dbSet;
            IQueryable<TEntity> result;

            if (filter != null)
                query = query.Where(filter);

            if (!String.IsNullOrWhiteSpace(includeProperties))
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(includeProperty).Where(filter);

            //Skip & Take
            page = page < 1 ? 0 : page - 1;
            int skip = page * records;
            int take = records;
            //var sql = query.ToSql();

            //Order by
            if (orderBy != null)
                result = orderBy(query)
                    .Skip(skip)
                    .Take(take)
                    .AsQueryable();
            else
                result = query
                    .OrderBy(m => true)
                    .Skip(skip)
                    .Take(take)
                    .AsQueryable();

            return result;
        }

        

        public virtual async Task<TEntity> GetFirstOrDefault(Expression<Func<TEntity, bool>> criteria)
        {
            return await DbContext.Set<TEntity>().FirstOrDefaultAsync(criteria);
        }

        public virtual async Task<IList<TEntity>> GetMultiple(Expression<Func<TEntity, bool>> criteria)
        {
            return await DbContext.Set<TEntity>().Where(criteria).ToListAsync();
        }

        public virtual async Task<IList<TEntity>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }
        
        public virtual async Task<Envelope<IEnumerable<TEntity>>> GetPaginate(PaginationDto paginationDto)
        {
            var collection =  await _dbSet.ToListAsync();
            
            
            var pagination = await _pageHelper.GetPage(collection, paginationDto);

            return pagination;
        }

        public virtual async Task<TEntity> Add(TEntity entity)
        {
            var obj = DbContext.Add(entity);

            await DbContext.SaveChangesAsync();

            return obj.Entity;
        }

        public virtual async Task<TEntity> Update(TEntity entity)
        {
            var obj = DbContext.Update(entity);

            await DbContext.SaveChangesAsync();

            return obj.Entity;
        }

        public virtual async Task Delete(TEntity entity)
        {
            DbContext.Remove(entity);

            await DbContext.SaveChangesAsync();
        }

    }
}