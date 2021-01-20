using System.Collections.Generic;
using System.Threading.Tasks;
using BankingApp.Domain.Entities;

namespace BankingApp.API.Helpers
{
    public interface ICustomerService {
        Task<Customer> GetById(int id);
        Task<IEnumerable<Customer>> GetAll();
        // Task<TEntity> Add<TEntity>(TEntity entity);
        Task<Customer> Update(Customer entity);
        // Task<TEntity> Delete<TEntity>(int id); //
    }
}
