using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class CartController : Controller
    {
        private ISweetRepository repository;
        private IOrderProcessor orderProcessor;

        public CartController(ISweetRepository repo, IOrderProcessor processor)
        {
            repository = repo;
            orderProcessor = processor;
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
                {
                    Cart = cart,
                    ReturnUrl = returnUrl
                });
        }

        public RedirectToRouteResult AddToCart(Cart cart, int sweetId, string returnUrl)
        {
            Sweet sweet = repository.Sweets
                .FirstOrDefault(s => s.SweetId == sweetId);

            if (sweet != null)
            {
                cart.AddItem(sweet, 1);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int sweetId, string returnUrl)
        {
            Sweet sweet = repository.Sweets
                .FirstOrDefault(s => s.SweetId == sweetId);

            if (sweet != null)
            {
                cart.RemoveLine(sweet);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Вибачте, кошик порожній!");
            }

            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(new ShippingDetails());
            }
        }

        public ActionResult Update(Cart, Update update)
            { 
                if (ModelState.IsValid)
                    {
                        var item = Sweet.FirstOrDefault(x => x.Id == update.SweetId && x.SweetId == update.SweetId);
                        Sweet.Update(x => x.Id == item.Id && x.SweetId == item.SweetId, item);
                        Sweet.SaveAndUpdate(); 
                        return RedirectToAction("Index");
                    }
                return View(update);
            }

    }
}