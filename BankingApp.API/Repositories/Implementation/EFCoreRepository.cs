using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using BankingApp.API.Repositories.Interfaces;
using BankingApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using BankingApp.API.Helpers;
using System;
using System.Linq.Expressions;
using System.Linq;
using BankingApp.API.Models.Pagination;
using BankingApp.API.Infrastructure.Extensions;

namespace BankingApp.API.Repositories {
    public class EFCoreRepository : IRepository
    {
        private readonly BankContext _context;
        private readonly IMapper _mapper;

        public EFCoreRepository(BankContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<T>> GetAll<T>() where T : BaseEntity
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetById<T>(int id) where T : BaseEntity
        {
            return await _context.FindAsync<T>(id);
        }
        public async Task<List<T>> GetWithInclude<T>(params Expression<Func<T, object>>[] props) where T : BaseEntity
        {
            var query = IncludeProperties(props);
            return await query.ToListAsync();
        }

        public async Task<T> Add<T>(T entity) where T : BaseEntity
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Update<T>(T entity) where T : BaseEntity
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Delete<T>(int id) where T : BaseEntity
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity == null) {
                throw new AppException($"{typeof(T)} could not be found");
            }

            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<bool> SaveAll() {
            return await _context.SaveChangesAsync() >= 0;
        }

        public async Task<List<T>> GetWithWhereList<T>(params Expression<Func<T, bool>>[] props) where T : BaseEntity
        {
            var query = WhereProperties(props);
            return await query.ToListAsync();
        }
        public async Task<T> GetWithWhere<T>(params Expression<Func<T, bool>>[] props) where T : BaseEntity
        {
            var query = WhereProperties(props);
            return await query.FirstOrDefaultAsync();
        }

        private IQueryable<TEntity> WhereProperties<TEntity>(params Expression<Func<TEntity, bool>>[] whereProperties) where TEntity: BaseEntity
        {
            IQueryable<TEntity> entities = _context.Set<TEntity>();
            foreach (var whereProperty in whereProperties)
            {
                entities = entities.Where(whereProperty);
            }
            return entities;
        }

        private IQueryable<T> IncludeProperties<T>(params Expression<Func<T, object>>[] props) where T: BaseEntity {
            IQueryable<T> entities = _context.Set<T>();
            foreach(var prop in props) {
                entities = entities.Include(prop);
            }
            return entities;
        }

        public async Task<PagedResponse<TModel>> GetPagedResponse<T, TModel>(PagedRequest request)
            where T : BaseEntity
            where TModel : class
        {
            return await _context.Set<T>().CreatePaginatedResultAsync<T, TModel>(request, _mapper);
        }

        public async Task<T> GetByIdWithInclude<T>(int id, params Expression<Func<T, object>>[] props) where T : BaseEntity
        {
            var query = IncludeProperties(props);
            return await query.FirstOrDefaultAsync(entity => entity.Id == id);
        }
    }
}
