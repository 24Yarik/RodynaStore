using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Domain.Abstract;
using Domain.Entities;
using WebUI.Controllers;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class ImageTests
    {
        [TestMethod]
        public void Can_Retrieve_Image_Data()
        {
            Sweet sweet = new Sweet
            {
                SweetId = 2,
                Name = "Sweet2",
                ImageData = new byte[] { },
                ImageMimeType = "image/png"
            };

            Mock<ISweetRepository> mock = new Mock<ISweetRepository>();
            mock.Setup(m => m.Sweets).Returns(new List<Sweet>
            {
                new Sweet {SweetId = 1, Name = "Sweet1"},
                sweet,
                new Sweet {SweetId = 3, Name = "Sweet3"}
            }.AsQueryable());

            SweetsController controller = new SweetsController(mock.Object);

            ActionResult result = controller.GetImage(2);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(FileResult));
            Assert.AreEqual(sweet.ImageMimeType, ((FileResult)result).ContentType);
        }

        [TestMethod]
        public void Cannot_Retrieve_Image_Data_For_Invalid_ID()
        {
            Mock<ISweetRepository> mock = new Mock<ISweetRepository>();
            mock.Setup(m => m.Sweets).Returns(new List<Sweet> {
                new Sweet {SweetId = 1, Name = "Sweet1"},
                new Sweet {SweetId = 2, Name = "Sweet2"}
            }.AsQueryable());

            SweetsController controller = new SweetsController(mock.Object);

            ActionResult result = controller.GetImage(10);

            Assert.IsNull(result);
        }

    }
}
