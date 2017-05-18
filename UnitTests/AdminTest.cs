using Domain.Abstract;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebUI.Controllers;

namespace UnitTests
{
    [TestClass]
    public class AdminTest
    {
        [TestMethod]
        public void Index_Contains_All_Sweets()
        {
            //Організація (arrange)
            Mock<ISweetRepository> mock = new Mock<ISweetRepository>();
            mock.Setup(m => m.Sweets).Returns(new List<Sweet>
                {
                    new Sweet{SweetId = 1, Name = "Sweet1"},
                    new Sweet{SweetId = 2, Name = "Sweet2"},
                    new Sweet{SweetId = 3, Name = "Sweet3"},
                    new Sweet{SweetId = 4, Name = "Sweet4"},
                    new Sweet{SweetId = 5, Name = "Sweet5"}
                });

            AdminController controller = new AdminController(mock.Object);

            //Дія (act)
            List<Sweet> result = ((IEnumerable<Sweet>)controller.Index().ViewData.Model).ToList();

            //Ствердження (assert)
            Assert.AreEqual(result.Count(), 5);
            Assert.AreEqual(result[0].Name, "Sweet1");
            Assert.AreEqual(result[1].Name, "Sweet2");
        }

        [TestMethod]
        public void Can_Edit_Sweet()
        {
            //Організація (arrange)
            Mock<ISweetRepository> mock = new Mock<ISweetRepository>();
            mock.Setup(m => m.Sweets).Returns(new List<Sweet>
                {
                    new Sweet{SweetId = 1, Name = "Sweet1"},
                    new Sweet{SweetId = 2, Name = "Sweet2"},
                    new Sweet{SweetId = 3, Name = "Sweet3"},
                    new Sweet{SweetId = 4, Name = "Sweet4"},
                    new Sweet{SweetId = 5, Name = "Sweet5"}
                });

            AdminController controller = new AdminController(mock.Object);

            //Дія (act)
            Sweet sweet1 = controller.Edit(1).ViewData.Model as Sweet;
            Sweet sweet2 = controller.Edit(2).ViewData.Model as Sweet;
            Sweet sweet3 = controller.Edit(3).ViewData.Model as Sweet;

            //Ствердження (assert)
            Assert.AreEqual(1, sweet1.SweetId);
            Assert.AreEqual(2, sweet2.SweetId);
            Assert.AreEqual(3, sweet3.SweetId);
        }

        [TestMethod]
        public void Cannot_Edit_Nonexistent_Sweet()
        {
            //Організація (arrange)
            Mock<ISweetRepository> mock = new Mock<ISweetRepository>();
            mock.Setup(m => m.Sweets).Returns(new List<Sweet>
                {
                    new Sweet{SweetId = 1, Name = "Sweet1"},
                    new Sweet{SweetId = 2, Name = "Sweet2"},
                    new Sweet{SweetId = 3, Name = "Sweet3"},
                    new Sweet{SweetId = 4, Name = "Sweet4"},
                    new Sweet{SweetId = 5, Name = "Sweet5"}
                });

            AdminController controller = new AdminController(mock.Object);

            //Дія (act)
            Sweet result = controller.Edit(7).ViewData.Model as Sweet;

            //Ствердження (assert)
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Can_Save_Valid_Changes()
        {
            Mock<ISweetRepository> mock = new Mock<ISweetRepository>();
            AdminController controller = new AdminController(mock.Object);

            Sweet sweet = new Sweet { Name = "Test" };

            ActionResult result = controller.Edit(sweet);

            mock.Verify(m => m.SaveSweet(sweet));

            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Can_Save_Invalid_Changes()
        {
            Mock<ISweetRepository> mock = new Mock<ISweetRepository>();
            AdminController controller = new AdminController(mock.Object);

            Sweet sweet = new Sweet { Name = "Test" };

            controller.ModelState.AddModelError("error", "error");

            ActionResult result = controller.Edit(sweet);

            mock.Verify(m => m.SaveSweet(It.IsAny<Sweet>()), Times.Never());

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
