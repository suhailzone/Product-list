using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProductList.Data;
using ProductList.Interfaces;
using ProductList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductList.Repository
{
    public class ProductRepository: IProductRepository
    {
        private readonly ProductContext dataContext;

        public ProductRepository (ProductContext productContext)
        {
            this.dataContext = productContext;
        }

        public bool AddProduct(Product product)
        {
            var result = ValidateAndAddProduct(product);
            if (result == null)
            {
                return false;
            }
            dataContext.SaveChanges();
            return true;
        }

        public bool DeleteProduct(int id)
        {
            var productToDelete = dataContext.Products.FirstOrDefault(p => p.ProductId == id);
            if (productToDelete == null)
            {
                return false;
            }
            dataContext.Products.Remove(productToDelete);
            dataContext.SaveChanges();
            return true;
        }

        public List<Category> GetAllCategory()
        {
            return dataContext.Categories.ToList();
        }

        public List<ProductDTO> GetAllProducts(int id, out int total)
        {
            var currentPageNumber = id == 1 || id == 0 ? 0 : id - 1;

            var productsDto = (from p in dataContext.Products
                               join c in dataContext.Categories on p.CategoryId equals c.CategoryId
                               select new ProductDTO
                               {
                                   CategoryName = c.CategoryName,
                                   CategoryId = p.CategoryId,
                                   ProductId = p.ProductId,
                                   ProductName = p.ProductName
                               }).ToList();
            total = productsDto.Count;
            var allProducts = productsDto.Select(s => s)
                     .Skip(currentPageNumber * 10)
                     .Take(10)
                     .ToList();
            return allProducts;
        }

        public Product GetProductById(int id)
        {
            return dataContext.Products.FirstOrDefault(p => p.ProductId == id);
        }

        public bool UpdateProduct(Product product, int id)
        {
            var productToUpdate = dataContext.Products.FirstOrDefault(p => p.ProductId == id);
            if (productToUpdate == null)
            {
                return false;
            }
            productToUpdate.ProductName = product.ProductName;
            productToUpdate.CategoryId = product.CategoryId;
            productToUpdate.ModifiedTimestamp = DateTime.Now;
            dataContext.SaveChanges();
            return true;
        }

        private EntityEntry<Product> ValidateAndAddProduct(Product product)
        {
            EntityEntry<Product> entityEntry = null;
            if(dataContext.Categories.Any(c => c.CategoryId == product.CategoryId))
            {
                entityEntry = dataContext.Products.Add(product);
            }
            return entityEntry;
        }
    }
}
