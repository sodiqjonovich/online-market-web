using Microsoft.AspNetCore.Mvc;
using OnlineMarket.Service.Common.Utils;
using OnlineMarket.Service.Interfaces.Products;

namespace OnlineMarket.Web.Controllers;

[Route("products")]
public class ProductsController : Controller
{
    private readonly IProductService _productService;
    private readonly int _pageSize = 30;
    public ProductsController(IProductService productService)
    {
        this._productService = productService;
    }
    public async Task<IActionResult> Index(int page = 1)
    {
        ViewBag.Qahhor = "Maxsulotlar v1";
        var products = await _productService.GetAllAsync(new PaginationParams(page, _pageSize));
        return View("Index", products);
    }
}
