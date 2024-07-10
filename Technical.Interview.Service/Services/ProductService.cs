using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Technical.Interview.Model;
using Technical.Interview.Model.Entities;

namespace Technical.Interview.Service
{
    /*Class core for all App operations*/
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductService> _logger;

        public ProductService(ApplicationDbContext context, ILogger<ProductService> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            try
            {
                return await _context.Product.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obteniendo productos");
                return Enumerable.Empty<Product>();
            }
        }

        public async Task<Product> GetUniqueProduct(int id)
        {
            try
            {
                return await _context.Product.FirstOrDefaultAsync(p => p.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error obteniendo el producto con id {id}.");
                return null;
            }
        }

        public async Task<Product> AddProduct(Product product)
        {
            try
            {
                _context.Product.Add(product);
                await _context.SaveChangesAsync(); 
                return product;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error agregando producto.");
                return null;
            }
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            try
            {
                _context.Product.Update(product);
                await _context.SaveChangesAsync(); 
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error actualizando producto.");
                return false;
            }
        }

        public async Task<bool> DeleteProduct(int id)
        {
            try
            {
                var product = await _context.Product.FindAsync(id);
                if (product == null)
                    return false;

                _context.Product.Remove(product);
                await _context.SaveChangesAsync(); 

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error eliminando producto con id {id}.");
                return false;
            }
        }
    }
}
