using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
 
namespace Ebook_Service.Abstract
{
    public  interface IGenericRepository<T> where T : class
    {
        Task <IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, Object>>[] expressions);
        T GetByIdAync(object id);
        Task<bool> AddAsync(T model);
        Task<bool> UpdateAsync(T model);
        Task<bool> DeleteAsync(int id);
    }
}
