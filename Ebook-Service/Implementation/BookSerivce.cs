using Ebook_Model;
using Ebook_Model.Model;
using Ebook_Service.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebook_Service.Implementation
{
    public class BookSerivce:GenericRepository<Book>, IBookSerivce
    {
        private readonly EbookDbContext _dbContext;
        public BookSerivce(EbookDbContext dbContext) : base(dbContext)
        {
            _dbContext= dbContext;
        }

        public async Task AddNewBookAsync(BookViewModel data)
        {
            var NewBook = new Book
            {
                Id= data.Id,
                BookName =data.BookName,
                price = data.price,
                TotalPages= data.TotalPages,
                Isbn= data.Isbn,
                Image= data.Image,
                GenreId = data.GenreId,
                PublisherId = data.PublisherId,
                AuthorId= data.AuthorId,

            };
            await _dbContext.Book.AddAsync(NewBook);
            await _dbContext.SaveChangesAsync();
        }

        public Task<Book> GetBookByIdAsync(int id)
        {
          var book = _dbContext.Book
                .Include(x => x.Author)
                .Include(x => x.Genre)
                .Include(x => x.Publisher)
                .FirstOrDefaultAsync(i => i.Id == id);
            return book;
        }

        public async Task<BooKDropDownVM> GetBookDropDownsValues()
        {
            var newDrop = new BooKDropDownVM()
            {
                Authors = await _dbContext.Author.OrderBy(n => n.AuthorName).ToListAsync(),
                  Genres = await  _dbContext.Genre.OrderBy(n => n.GenreName).ToListAsync(),
                Publisher = await  _dbContext.Publisher.OrderBy(n => n.PublisherName).ToListAsync(),
            };
            return newDrop;
        }

        public async Task UpdateBookAsync(BookViewModel data)
        {
            var db = await _dbContext.Book.FirstOrDefaultAsync(n => n.Id == data.Id);
            if (db != null)
            {
                db.BookName = data.BookName;
                db.price = data.price;
                db.TotalPages= data.TotalPages;
                db.price = data.price;
                db.Isbn = data.Isbn;
                db.AuthorId= data.AuthorId;   
                db.GenreId = data.GenreId;
                db.PublisherId= data.PublisherId;
                await _dbContext.SaveChangesAsync();
            }
             
        }
    }
}
