using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Entities.DomainModels;
using Microsoft.AspNetCore.Mvc;
using MvcWebUI.Helpers;
using MvcWebUI.Models;

namespace MvcWebUI.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService cartService;
        private readonly ICartSessionHelper cartSessionHelper;
        private readonly IProductService productService;

        public CartController(ICartService cartService, ICartSessionHelper cartSessionHelper, IProductService productService)
        {
            this.cartService = cartService;
            this.cartSessionHelper = cartSessionHelper;
            this.productService = productService;
        }

        public IActionResult AddToCart(int productId)
        {
            Product product = productService.GetById(productId);

            var cart = cartSessionHelper.GetCart("cart");

            cartService.AddToCart(cart, product);

            cartSessionHelper.SetCart(key: "cart", cart);

            TempData.Add("message", product.ProductName + " sepete eklendi.");

            return Redirect(@"/product/index?category=" + product.CategoryId);

            //return RedirectToAction("Index", "Product");
        }
        public IActionResult RemoveFromCart(int productId)
        {
            Product product = productService.GetById(productId);
            var cart = cartSessionHelper.GetCart(key: "cart");
            cartService.RemoveFromCart(cart, productId);
            cartSessionHelper.SetCart(key: "cart", cart);

            TempData.Add("message", product.ProductName + " sepetten silindi.");

            return RedirectToAction("Index", "Cart");
        }

        public IActionResult Index()
        {
            var model = new CartListViewModel
            {
                Cart = cartSessionHelper.GetCart(key: "cart")
            };
            return View(model);
        }

        public IActionResult Complete()
        {
            var model = new ShippingDetailsViewModel
            {
                ShippingDetail = new ShippingDetail()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Complete(ShippingDetail shippingDetail)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            TempData.Add("message", "Siparişiniz başarıyla tamamlandı.");
            cartSessionHelper.Clear();
            return RedirectToAction("Index", "Cart");
        }
    }
}
