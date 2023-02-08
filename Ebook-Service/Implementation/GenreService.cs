using Ebook_Model;
using Ebook_Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebook_Service.Implementation
{
    public  class GenreService:GenericRepository<Genre>,IGenreService
    {
        private readonly EbookDbContext _ebookDb;
        public GenreService(EbookDbContext dbContext):base(dbContext)
        {
            _ebookDb= dbContext;
        }

        public Genre finbyId(int id)
        {
            return _ebookDb.Genre.Find(id);
        }
    }
}
