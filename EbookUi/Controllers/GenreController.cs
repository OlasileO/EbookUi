using Ebook_Model;
using Ebook_Service.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace EbookUi.Controllers
{
    public class GenreController : Controller
    {
        private readonly IGenreService _service;

        public GenreController(IGenreService service)
        {
            _service=service;
        }
        public async Task<IActionResult> Index(string search)
        {
            var result = await _service.GetAllAsync();
            if (!string.IsNullOrWhiteSpace(search))
            {
                result = result.Where(b => b.GenreName.ToLower().Contains(search)).ToList();
            }
            return View("Index", result);


        }
        public async Task<IActionResult> Create()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Genre model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = await _service.AddAsync(model);
            if (result)
            {
                TempData["msg"] = "Added successfuly";
                return RedirectToAction("Create");
            }
            TempData["msg"] = "Error has occur on the server side";
            return View(model);
        }
        public IActionResult Edit(int id)
        {
            var result = _service.GetByIdAync(id);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Genre model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _service.UpdateAsync(model);
            if (result)
            {
                TempData["msg"] = "Added successfuly";
                return RedirectToAction("Edit");
            }
            TempData["msg"] = "Error has occur on the server side";
            return View(model);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
