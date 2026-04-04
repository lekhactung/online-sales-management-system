using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    //interface chung, mọi repo sẽ implement cái này
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(string id);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(string id);

    }
}
