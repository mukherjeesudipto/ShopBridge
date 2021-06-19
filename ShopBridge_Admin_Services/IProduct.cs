using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopBridge_Admin_DataAccess;

namespace ShopBridge_Admin_Services
{
    public interface IProduct
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductById(int id);
        Task SaveProduct(Product item);
        Task DeleteProduct(int id);
        Task<List<Product>> GetProductByCategory(string categoryName);
        Task UpdateProduct(int id, Product item);
    }
}
