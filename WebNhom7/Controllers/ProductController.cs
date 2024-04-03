using Microsoft.AspNetCore.Mvc;
using WebNhom7.Models;
using WebNhom7.Repositories;

namespace WebNhom7.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository; 
        }
        // Hiển thị danh sách sản phẩm
        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAllAsync();
            return View(products);
    }
}
}
