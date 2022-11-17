using ProductList.Data;
using ProductList.Interfaces;
using ProductList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductList.Repository
{
    public class CategoryRepository: ICategoryRepository
    {
        private readonly ProductContext dataContext;

        public CategoryRepository(ProductContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public bool AddCategory(Category category)
        {
            var result = dataContext.Categories.Add(category);
            if (result == null)
            {
                return false;
            }
            dataContext.SaveChanges();
            return true;
        }

        public bool DeleteCategory(int id)
        {
            var categoryToRemove = dataContext.Categories.FirstOrDefault(category => category.CategoryId == id);
            if (categoryToRemove == null)
            {
                return false;
            }
            dataContext.Products.RemoveRange(dataContext.Products.Where(p => p.CategoryId == id));
            dataContext.Categories.Remove(categoryToRemove);
            dataContext.SaveChanges();
            return true;
        }

        public List<Category> GetAllCategory()
        {
            return dataContext.Categories.ToList();
        }

        public Category GetCategoryById(int id)
        {
            return dataContext.Categories.FirstOrDefault(c => c.CategoryId == id);
        }

        public bool UpdateCategory(Category category, int id)
        {
            var categoryToUpdate = dataContext.Categories.FirstOrDefault(category => category.CategoryId == id);
            if (categoryToUpdate == null)
            {
                return false;
            }
            categoryToUpdate.CategoryName = category.CategoryName;
            categoryToUpdate.ModifiedTimestamp = DateTime.Now;
            dataContext.SaveChanges();
            return true;
        }
    }
}
