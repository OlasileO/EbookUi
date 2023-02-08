using Ebook_Model.Model;
using Ebook_Service.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EbookUi.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookSerivce _serivce;

        public BookController(IBookSerivce serivce)
        {
            _serivce=serivce;
        }

        public async Task<IActionResult> Index( string search)
        {
            var allBook = await _serivce.GetAllAsync(a => a.Author, g => g.Genre, p => p.Publisher);
            if (!string.IsNullOrWhiteSpace(search))
            {
                allBook = allBook.Where(b => b.BookName.ToLower().Contains(search) ||
                b.Genre.GenreName.ToLower().Contains(search)).ToList();
            }
            return View("Index",allBook);
        }
        public async Task<IActionResult> Add(string search)
        {
            var allBook = await _serivce.GetAllAsync(a => a.Author, g => g.Genre, p => p.Publisher);
            if (!string.IsNullOrWhiteSpace(search))
            {
                allBook = allBook.Where(b => b.BookName.ToLower().Contains(search) ||
                b.Genre.GenreName.ToLower().Contains(search)).ToList();
            }
            return View("Add", allBook);
        }
        public async Task<IActionResult> Create()
        {
            var newbook = await _serivce.GetBookDropDownsValues();
            ViewBag.Authors = new SelectList(newbook.Authors, "Id", "AuthorName");
            ViewBag.Genres = new SelectList(newbook.Genres, "Id", "GenreName");
            ViewBag.Publishers = new SelectList(newbook.Publisher, "Id", "PublisherName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(BookViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var newbook = await _serivce.GetBookDropDownsValues();
                ViewBag.Authors = new SelectList(newbook.Authors, "Id", "AuthorName");
                ViewBag.Genres = new SelectList(newbook.Genres, "Id", "GenreName");
                ViewBag.Publishers = new SelectList(newbook.Publisher, "Id", "PublisherName");
                return View(viewModel);
            }

            await  _serivce.AddNewBookAsync(viewModel);
                TempData["msg"] = "Added successfuly";
            return RedirectToAction("Create");
          
        }

        public async Task<IActionResult> Edit(int id)
        {
            var bookEdit = await _serivce.GetBookByIdAsync(id);
            var reponse = new BookViewModel()
            {
                Id = bookEdit.Id,
                BookName = bookEdit.BookName,
                price=bookEdit.price,
                Isbn =bookEdit.Isbn,
                Image = bookEdit.Image,
                TotalPages = bookEdit.TotalPages,
                AuthorId = bookEdit.AuthorId,
                GenreId = bookEdit.GenreId,
                PublisherId = bookEdit.PublisherId
            };
            var newbook = await _serivce.GetBookDropDownsValues();
            ViewBag.Authors = new SelectList(newbook.Authors, "Id", "AuthorName");
            ViewBag.Genres = new SelectList(newbook.Genres, "Id", "GenreName");
            ViewBag.Publishers = new SelectList(newbook.Publisher, "Id", "PublisherName");
            return View(reponse);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BookViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var newbook = await _serivce.GetBookDropDownsValues();
                ViewBag.Authors = new SelectList(newbook.Authors, "Id", "AuthorName");
                ViewBag.Genres = new SelectList(newbook.Genres, "Id", "GenreName");
                ViewBag.Publishers = new SelectList(newbook.Publisher, "Id", "PublisherName");
                return View(viewModel);
            }
             await  _serivce.UpdateBookAsync(viewModel);
            TempData["msg"] = "Added successfuly";
            return RedirectToAction("Edit");
        }

        public async Task<IActionResult> Delete(int id)
        {
             await _serivce.DeleteAsync(id);
            return RedirectToAction("Index");

        }
        public IActionResult Privacy ()
        {
            return View();
        }
    }
}
