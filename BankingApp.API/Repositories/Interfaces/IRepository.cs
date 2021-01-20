using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using BankingApp.Domain.Entities;
using System.Linq.Expressions;
using BankingApp.API.Models.Pagination;

namespace BankingApp.API.Repositories.Interfaces {
    public interface IRepository {
        Task<T> GetById<T>(int id) where T: BaseEntity;
        Task<List<T>> GetAll<T>() where T: BaseEntity;
        Task<PagedResponse<TModel>> GetPagedResponse<T, TModel>(PagedRequest request) where T: BaseEntity where TModel: class;
        Task<T> GetWithWhere<T>(params Expression<Func<T, bool>>[] props ) where T: BaseEntity;
        Task<List<T>> GetWithWhereList<T>(params Expression<Func<T, bool>>[] props) where T: BaseEntity;
        Task<List<T>> GetWithInclude<T>(params Expression<Func<T, object>>[] props) where T: BaseEntity;
        Task<T> GetByIdWithInclude<T>(int id, params Expression<Func<T, object>>[] props) where T: BaseEntity;
        Task<T> Add<T>(T entity) where T: BaseEntity;
        Task<T> Update<T> (T entity) where T: BaseEntity;
        Task<T> Delete<T> (int id) where T: BaseEntity;
        Task<bool> SaveAll();
    }
}
