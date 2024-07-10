using System.Collections.Generic;
using System.Threading.Tasks;
using Technical.Interview.Model.Entities;

namespace Technical.Interview.Service
{
    /*Interface with methods definitions*/
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetUniqueProduct(int id);
        Task<Product> AddProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(int id);
    }
}
