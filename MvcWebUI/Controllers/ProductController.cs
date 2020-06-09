using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using MvcWebUI.Models;

namespace MvcWebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        public IActionResult Index(int category)
        {
            var model = new ProductListViewModel
            {
                Products = category > 0 ? productService.GetByCategory(category) : productService.GetAll()
            };
            return View(model);
        }
    }
}
