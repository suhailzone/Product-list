using ProductList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductList.Interfaces
{
    public interface ICategoryRepository
    {
        List<Category> GetAllCategory();

        Category GetCategoryById(int id);

        bool AddCategory(Category category);

        bool UpdateCategory(Category category, int id);

        bool DeleteCategory(int id);
    }
}
