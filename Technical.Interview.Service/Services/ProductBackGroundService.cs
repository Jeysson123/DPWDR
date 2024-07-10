using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Technical.Interview.Model;
using Technical.Interview.Model.Entities;

namespace Technical.Interview.Service
{
    /*Background worker for add products, that don't exist in local database*/
    public class ProductBackGroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<ProductBackGroundService> _logger;
        private readonly string _apiUrl = "https://fakestoreapi.com/products";
        private readonly int _intervalInMilliseconds = 1800000; // 30 minute interval
        private readonly HashSet<int> _deletedProductIds = new HashSet<int>();

        public ProductBackGroundService(IServiceScopeFactory scopeFactory, ILogger<ProductBackGroundService> logger)
        {
            _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Validando productos...");
                await ValidateProducts();
                await Task.Delay(_intervalInMilliseconds, stoppingToken);
            }
        }

        private async Task ValidateProducts()
        {
            try
            {
                var productsFromApi = await FetchProductsFromApi();

                if (productsFromApi == null || !productsFromApi.Any())
                {
                    _logger.LogWarning("No hay productos.");
                    return;
                }

                using (var scope = _scopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    var existingProducts = await context.Product.ToListAsync();

                    var newProducts = productsFromApi.Where(apiProduct =>
                        !existingProducts.Any(dbProduct =>
                            dbProduct.Title == apiProduct.Title
                        ) && !_deletedProductIds.Contains(apiProduct.Id)
                    ).ToList();

                    if (newProducts.Any())
                    {
                        var productsToAdd = newProducts.Select(p => new Product
                        {
                            Title = p.Title,
                            Price = p.Price,
                            Description = p.Description,
                            Category = p.Category
                        }).ToList();

                        context.Product.AddRange(productsToAdd);
                        await context.SaveChangesAsync(); 

                        _logger.LogInformation($"{productsToAdd.Count} productos agregados.");
                    }
                    else
                    {
                        _logger.LogInformation("No existen nuevos productos.");
                    }

                    var productsToRemove = existingProducts.Where(dbProduct =>
                        !productsFromApi.Any(apiProduct =>
                            apiProduct.Title == dbProduct.Title
                        )
                    ).ToList();

                    if (productsToRemove.Any())
                    {
                        context.Product.RemoveRange(productsToRemove);
                        await context.SaveChangesAsync();

                        _logger.LogInformation($"{productsToRemove.Count} productos eliminados.");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error validando productos.");
            }
        }

        private async Task<List<Product>> FetchProductsFromApi()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync(_apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var products = JsonConvert.DeserializeObject<List<Product>>(content);
                        return products;
                    }
                    else
                    {
                        _logger.LogError($"Falla en la obtencion de productos: {response.StatusCode}");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obteniendo productos.");
                return null;
            }
        }
    }
}
