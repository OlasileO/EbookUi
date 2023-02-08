using Ebook_Model;
using Ebook_Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebook_Service.Abstract
{
    public interface IBookSerivce:IGenericRepository<Book>
    {
        Task<Book> GetBookByIdAsync(int id);
        Task<BooKDropDownVM> GetBookDropDownsValues();

        Task AddNewBookAsync(BookViewModel data);

        Task UpdateBookAsync(BookViewModel data);
    }
}
