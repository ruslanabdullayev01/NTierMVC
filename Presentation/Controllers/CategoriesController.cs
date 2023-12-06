using Business.Services.Abstract;
using Business.ViewModels;
using Common.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _service.GetAllAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateVM model)
        {
            var result = await _service.CreateAsync(model);
            if (!result)
            {
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Update(CategoryUpdateVM model, int id)
        {
            bool result = await _service.Update(model, id);
            if (!result)
            {
                return View(model);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await _service.Delete(id);
            if (!result)
            {
                return NotFound();
            }
            return RedirectToAction("Index"); 
        }
    }
}
