using ECommerce_WebApp.Models;
using ECommerce_WebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using ECommerce_WebApp.Operations.Models;
namespace ECommerce_WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public HomeController(
            ILogger<HomeController> logger,
            ICategoryService categoryService,
            IProductService productService)
        {
            _logger = logger;
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var username = User.Identity?.IsAuthenticated == true ? User.Identity.Name : null;
            ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();
            var viewModel = new HomeViewModel
            {
                FeaturedCategories = await _categoryService.GetFeaturedCategoriesAsync(),
                FeaturedProducts = await _productService.GetFeaturedProductsAsync(),
                BestSellers = await _productService.GetBestSellersAsync(),
                Recommendations = await _productService.GetRecommendationsAsync(username)
            };
            return View(viewModel);
        }

        

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}
