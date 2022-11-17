using Microsoft.AspNetCore.Mvc;
using ProductList.Data;
using ProductList.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductList.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            return View(categoryRepository.GetAllCategory());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string categoryName)
        {
            categoryRepository.AddCategory(new Models.Category { CategoryName = categoryName, CreatedTimestamp = DateTime.Now });
            return RedirectToAction("Index", "Categories");
        }

        public IActionResult Edit(int id)
        {
            return View(categoryRepository.GetCategoryById(id));
        }

        [HttpPost]
        public IActionResult Edit(int categoryId, string categoryName)
        {
            categoryRepository.UpdateCategory(new Models.Category { CategoryName = categoryName, CreatedTimestamp = DateTime.Now }, categoryId);
            return RedirectToAction("Index", "Categories");
        }

        public IActionResult Delete(int id)
        {
            return View(categoryRepository.GetCategoryById(id));
        }

        [HttpPost]
        public IActionResult DeleteCategory(int categoryId)
        {
            categoryRepository.DeleteCategory(categoryId);
            return RedirectToAction("Index", "Categories");
        }
    }
}
