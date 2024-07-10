using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Technical.Interview.Client.Models;
using Technical.Interview.Model.Entities;
using System.Net.Http.Json;

namespace Technical.Interview.Client.Controllers
{
    /*Controller that manage all views workflow with the different requests*/
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string _url;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _url = configuration.GetConnectionString("ProductApiUrl");
        }

        private async Task<List<Product>> GetProducts()
        {
            List<Product> listProducts = new List<Product>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_url))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    listProducts = JsonConvert.DeserializeObject<List<Product>>(apiResponse);
                }
            }

            return listProducts;
        }

        public async Task<IActionResult> Index()
        {
            List<Product> listProducts = await GetProducts();
            ProductAdapter adapter = new ProductAdapter
            {
                Product = new Product()
            };

            if (listProducts.Count > 0)
                adapter.ListProducts = listProducts;
            else
                TempData["listEmpty"] = "No hay productos registrados.";

            return View(adapter);
        }

        public async Task<IActionResult> CreateProduct(Product product)
        {
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.PostAsJsonAsync(_url, product);
                try
                {
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            TempData["Registered"] = "Transacción realizada exitosamente.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DetailsProduct(int id)
        {
            Product product = new Product();
            ProductAdapter adapter = new ProductAdapter();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_url + id))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        product = JsonConvert.DeserializeObject<Product>(apiResponse);
                    }
                    else
                        ViewBag.StatusCode = response.StatusCode;
                }
            }

            adapter.Product = product;
            adapter.ListProducts = await GetProducts();
            TempData["Details"] = "Yes";
            if (adapter.ListProducts == null) TempData["listEmpty"] = "No hay productos registrados.";

            return View(nameof(Index), adapter);
        }

        public async Task<IActionResult> PreparedProduct(int id)
        {
            Product product = new Product();
            ProductAdapter adapter = new ProductAdapter();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_url + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    product = JsonConvert.DeserializeObject<Product>(apiResponse);
                }
            }

            adapter.Product = product;
            adapter.ListProducts = await GetProducts();
            TempData["ReadyUpdate"] = "Yes";

            if (adapter.ListProducts == null) TempData["listEmpty"] = "No hay productos registrados.";

            return View(nameof(Index), adapter);
        }

        public async Task<IActionResult> UpdateProduct(Product product)
        {
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.PutAsJsonAsync($"{_url}{product.Id}", product);
                if (response.IsSuccessStatusCode)
                {
                    ProductAdapter adapter = new ProductAdapter
                    {
                        Product = product,
                        ListProducts = await GetProducts()
                    };
                    if (adapter.ListProducts == null) TempData["listEmpty"] = "No hay productos registrados.";
                    TempData["Updated"] = "Transacción realizada exitosamente.";
                    return View(nameof(Index), adapter);
                }
                else
                {
                    TempData["Updated"] = "Error al ejecutar la transacción. Intente más tarde.";
                    return RedirectToAction(nameof(Index));
                }
            }
        }

        public async Task<IActionResult> DeleteProduct(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync(_url + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            TempData["Deleted"] = "Transacción realizada exitosamente.";
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
