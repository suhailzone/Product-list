using Microsoft.AspNetCore.Mvc;
using ProductList.Data;
using ProductList.Interfaces;
using ProductList.Models;
using ProductList.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductList.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProductList(int id, string productName)
        {
            var allProducts = productRepository.GetAllProducts(id, out var total);
            bool isNextDisable;
            bool isPrevDisable;
            int pageNumber;
            if (id == 0 || id == 1)
            {
                isNextDisable = total > 10;
                pageNumber = 1;
                isPrevDisable = false;
            }
            else
            {
                pageNumber = id;
                isNextDisable = total > id * 10;
                isPrevDisable = true;
            }
            if(productName != null)
            {
                ViewBag.Alert = "<div class='alert alert-success alert-dismissable' id='alert'><button type='button' class='close' data-dismiss='alert'>×</button><strong> Success!</ strong > " + productName + " Added" + "</a>.</div>";
            }
            ViewData["prevPage"] = pageNumber - 1;
            ViewData["nextPage"] = pageNumber + 1;
            ViewData["pageNumber"] = pageNumber;
            ViewData["nextEnable"] = !isNextDisable;
            ViewData["prevEnable"] = !isPrevDisable;
            return View(allProducts);
        }

        public IActionResult Create()
        {
            var categories = productRepository.GetAllCategory();
            return View(categories);
        }

        [HttpPost]
        public IActionResult Create(int categoryId, string productName)
        {
            productRepository.AddProduct(new Product { ProductName = productName, CategoryId = categoryId, CreatedTimestamp = DateTime.Now });
            return RedirectToAction("ProductList", "Products", new { id = 0, productName = productName });
        }

        public IActionResult Edit(int id)
        {
            ViewData["list"] = productRepository.GetAllCategory();
            return View(productRepository.GetProductById(id));
        }

        [HttpPost]
        public IActionResult Edit(int categoryId, int productId, string productName)
        {
            productRepository.UpdateProduct(new Models.Product { CategoryId = categoryId, ProductName = productName, CreatedTimestamp = DateTime.Now }, productId);
            return RedirectToAction("ProductList", "Products");
        }

        public IActionResult Delete(int id)
        {
            return View(productRepository.GetProductById(id));
        }

        [HttpPost]
        public IActionResult DeleteCategory(int productId)
        {
            productRepository.DeleteProduct(productId);
            return RedirectToAction("ProductList", "Products");
        }
    }
}
