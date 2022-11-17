using ProductList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductList.Interfaces
{
    public interface IProductRepository
    {
        List<ProductDTO> GetAllProducts(int id, out int total);

        List<Category> GetAllCategory();

        Product GetProductById(int id);

        bool AddProduct(Product product);

        bool UpdateProduct(Product product, int id);

        bool DeleteProduct(int id);
    }
}
