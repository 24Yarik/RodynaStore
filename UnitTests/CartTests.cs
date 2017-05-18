using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Domain.Abstract;
using WebUI.Controllers;
using System.Web.Mvc;
using WebUI.Models;

namespace UnitTests
{
    [TestClass]
    public class CartTests
    {
        [TestMethod]
        public void Can_Add_New_Lines()
        {
            //Організація
            Sweet sweet1 = new Sweet { SweetId = 1, Name = "Sweet1" };
            Sweet sweet2 = new Sweet { SweetId = 2, Name = "Sweet2" };

            Cart cart = new Cart();

            //Дія
            cart.AddItem(sweet1, 1);
            cart.AddItem(sweet2, 1);
            List<CartLine> results = cart.Lines.ToList();

            //Ствердження
            Assert.AreEqual(results.Count(), 2);
            Assert.AreEqual(results[0].Sweet, sweet1);
            Assert.AreEqual(results[1].Sweet, sweet2);
        }

        [TestMethod]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            //Організація
            Sweet sweet1 = new Sweet { SweetId = 1, Name = "Sweet1" };
            Sweet sweet2 = new Sweet { SweetId = 2, Name = "Sweet2" };

            Cart cart = new Cart();

            //Дія
            cart.AddItem(sweet1, 1);
            cart.AddItem(sweet2, 1);
            cart.AddItem(sweet1, 5);
            List<CartLine> results = cart.Lines.OrderBy(c => c.Sweet.SweetId).ToList();

            //Ствердження
            Assert.AreEqual(results.Count(), 2);
            Assert.AreEqual(results[0].Quantity, 6);
            Assert.AreEqual(results[1].Quantity, 1);
        }

        [TestMethod]
        public void Can_Remove_Line()
        {
            //Організація
            Sweet sweet1 = new Sweet { SweetId = 1, Name = "Sweet1" };
            Sweet sweet2 = new Sweet { SweetId = 2, Name = "Sweet2" };
            Sweet sweet3 = new Sweet { SweetId = 3, Name = "Sweet3" };

            Cart cart = new Cart();

            //Дія
            cart.AddItem(sweet1, 1);
            cart.AddItem(sweet2, 1);
            cart.AddItem(sweet1, 5);
            cart.AddItem(sweet3, 2);
            cart.RemoveLine(sweet2);

            //Ствердження
            Assert.AreEqual(cart.Lines.Where(c => c.Sweet == sweet2).Count(), 0);
            Assert.AreEqual(cart.Lines.Count(), 2);
        }

        [TestMethod]
        public void Calculate_Cart_Total()
        {
            //Організація
            Sweet sweet1 = new Sweet { SweetId = 1, Name = "Sweet1", Price = 100 };
            Sweet sweet2 = new Sweet { SweetId = 2, Name = "Sweet2", Price = 55 };

            Cart cart = new Cart();

            //Дія
            cart.AddItem(sweet1, 1);
            cart.AddItem(sweet2, 1);
            cart.AddItem(sweet1, 5);
            decimal result = cart.ComputeTotalValue();

            //Ствердження
            Assert.AreEqual(result, 655);
        }

        [TestMethod]
        public void Can_Clear_Contents()
        {
            //Організація
            Sweet sweet1 = new Sweet { SweetId = 1, Name = "Sweet1", Price = 100 };
            Sweet sweet2 = new Sweet { SweetId = 2, Name = "Sweet2", Price = 55 };

            Cart cart = new Cart();

            //Дія
            cart.AddItem(sweet1, 1);
            cart.AddItem(sweet2, 1);
            cart.AddItem(sweet1, 5);
            cart.Clear();

            //Ствердження
            Assert.AreEqual(cart.Lines.Count(), 0);
        }

        //Додавання елемента в корзину
        [TestMethod]
        public void Can_Add_To_Cart()
        {
            Mock<ISweetRepository> mock = new Mock<ISweetRepository>();
            mock.Setup(m => m.Sweets).Returns(new List<Sweet>
            {
                new Sweet {SweetId = 1, Name = "Sweet1", Type = "Type1"}
            }.AsQueryable());

            Cart cart = new Cart();

            CartController controller = new CartController(mock.Object,null);
            controller.AddToCart(cart, 1, null);

            Assert.AreEqual(cart.Lines.Count(), 1);
            Assert.AreEqual(cart.Lines.ToList()[0].Sweet.SweetId, 1);
        }

        //Поле додавання товару в кошик - перенаправлення на сторінку кошика
        [TestMethod]
        public void Adding_Sweet_To_Cart_Goes_To_Cart_Screen()
        {
            Mock<ISweetRepository> mock = new Mock<ISweetRepository>();
            mock.Setup(m => m.Sweets).Returns(new List<Sweet>
            {
                new Sweet {SweetId = 1, Name = "Sweet1", Type = "Type1"}
            }.AsQueryable());

            Cart cart = new Cart();

            CartController controller = new CartController(mock.Object,null);

            RedirectToRouteResult result = controller.AddToCart(cart, 2, "myUrl");

            Assert.AreEqual(result.RouteValues["action"], "Index");
            Assert.AreEqual(result.RouteValues["returnUrl"], "myUrl");
        }

        [TestMethod]
        public void Can_View_Cart_Contents()
        {
            Cart cart = new Cart();
            CartController target = new CartController(null,null);

            CartIndexViewModel result = (CartIndexViewModel) target.Index(cart, "myUrl").ViewData.Model;

            Assert.AreSame(result.Cart, cart);
            Assert.AreEqual(result.ReturnUrl, "myUrl");
        }

        [TestMethod]
        public void Cannot_Checkout_Empty_Cart()
        {
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
            Cart cart = new Cart();
            ShippingDetails shippingDetails = new ShippingDetails();

            CartController controller = new CartController(null, mock.Object);

            ViewResult result = controller.Checkout(cart, shippingDetails);

            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Never());

            Assert.AreEqual("", result.ViewName);
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void Cannot_Checkout_Invalid_ShippingDetails()
        {
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
            Cart cart = new Cart();
            cart.AddItem(new Sweet(), 1);

            CartController controller = new CartController(null, mock.Object);
            controller.ModelState.AddModelError("error", "error");

            ViewResult result = controller.Checkout(cart, new ShippingDetails());

            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Never());

            Assert.AreEqual("", result.ViewName);
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void Cannot_Checkout_And_Submit_Order()
        {
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
            Cart cart = new Cart();
            cart.AddItem(new Sweet(), 1);

            CartController controller = new CartController(null, mock.Object);

            ViewResult result = controller.Checkout(cart, new ShippingDetails());

            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Once());

            Assert.AreEqual("Completed", result.ViewName);
            Assert.AreEqual(true, result.ViewData.ModelState.IsValid);
        }
    }
}
