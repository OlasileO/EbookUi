using Ebook_Model;
using Ebook_Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebook_Service.Implementation
{
    public class AuthorService:GenericRepository<Author>,IAuthorService
    {
        public AuthorService(EbookDbContext dbContext) : base(dbContext)
        {

        }
    }
}
