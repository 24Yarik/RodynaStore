using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Domain.Abstract;
using System.Collections.Generic;
using Domain.Entities;
using WebUI.Controllers;
using System.Linq;
using System.Web.Mvc;
using WebUI.Models;
using WebUI.HtmlHelpers;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            //Організація (arrange)
            Mock<ISweetRepository> mock = new Mock<ISweetRepository>();
            mock.Setup(m => m.Sweets).Returns(new List<Sweet>
                {
                    new Sweet{SweetId = 1, Name = "Sweet1"},
                    new Sweet{SweetId = 2, Name = "Sweet2"},
                    new Sweet{SweetId = 3, Name = "Sweet3"},
                    new Sweet{SweetId = 4, Name = "Sweet4"},
                    new Sweet{SweetId = 5, Name = "Sweet5"},
                    new Sweet{SweetId = 6, Name = "Sweet6"},
                    new Sweet{SweetId = 7, Name = "Sweet7"},
                    new Sweet{SweetId = 8, Name = "Sweet8"},
                    new Sweet{SweetId = 9, Name = "Sweet9"},
                    new Sweet{SweetId = 10, Name = "Sweet10"},
                    new Sweet{SweetId = 11, Name = "Sweet11"},
                    new Sweet{SweetId = 12, Name = "Sweet12"},
                    new Sweet{SweetId = 13, Name = "Sweet13"},
                    new Sweet{SweetId = 14, Name = "Sweet14"},
                    new Sweet{SweetId = 15, Name = "Sweet15"},
                    new Sweet{SweetId = 16, Name = "Sweet16"},
                    new Sweet{SweetId = 17, Name = "Sweet17"},
                    new Sweet{SweetId = 18, Name = "Sweet18"},
                    new Sweet{SweetId = 19, Name = "Sweet19"},
                    new Sweet{SweetId = 20, Name = "Sweet20"},
                    new Sweet{SweetId = 21, Name = "Sweet21"},
                    new Sweet{SweetId = 22, Name = "Sweet22"},
                    new Sweet{SweetId = 23, Name = "Sweet23"},
                    new Sweet{SweetId = 24, Name = "Sweet24"},
                    new Sweet{SweetId = 25, Name = "Sweet25"},
                    new Sweet{SweetId = 26, Name = "Sweet26"},
                    new Sweet{SweetId = 27, Name = "Sweet27"},
                    new Sweet{SweetId = 28, Name = "Sweet28"},
                    new Sweet{SweetId = 29, Name = "Sweet29"},
                    new Sweet{SweetId = 30, Name = "Sweet30"},
                    new Sweet{SweetId = 31, Name = "Sweet31"},
                    new Sweet{SweetId = 32, Name = "Sweet32"},
                    new Sweet{SweetId = 33, Name = "Sweet33"},
                    new Sweet{SweetId = 34, Name = "Sweet34"},
                    new Sweet{SweetId = 35, Name = "Sweet35"},
                    new Sweet{SweetId = 36, Name = "Sweet36"},
                    new Sweet{SweetId = 37, Name = "Sweet37"},
                    new Sweet{SweetId = 38, Name = "Sweet38"},
                    new Sweet{SweetId = 39, Name = "Sweet39"},
                    new Sweet{SweetId = 40, Name = "Sweet40"},
                    new Sweet{SweetId = 41, Name = "Sweet41"},
                    new Sweet{SweetId = 42, Name = "Sweet42"},
                    new Sweet{SweetId = 43, Name = "Sweet43"},
                    new Sweet{SweetId = 44, Name = "Sweet44"},
                    new Sweet{SweetId = 45, Name = "Sweet45"},
                    new Sweet{SweetId = 46, Name = "Sweet46"},
                    new Sweet{SweetId = 47, Name = "Sweet47"},
                    new Sweet{SweetId = 48, Name = "Sweet48"},
                    new Sweet{SweetId = 49, Name = "Sweet49"},
                    new Sweet{SweetId = 50, Name = "Sweet50"},
                    new Sweet{SweetId = 51, Name = "Sweet51"},
                    new Sweet{SweetId = 52, Name = "Sweet52"},
                    new Sweet{SweetId = 53, Name = "Sweet53"},
                    new Sweet{SweetId = 54, Name = "Sweet54"},
                    new Sweet{SweetId = 55, Name = "Sweet55"},
                    new Sweet{SweetId = 56, Name = "Sweet56"},
                    new Sweet{SweetId = 57, Name = "Sweet57"},
                    new Sweet{SweetId = 58, Name = "Sweet58"},
                    new Sweet{SweetId = 59, Name = "Sweet59"},
                    new Sweet{SweetId = 60, Name = "Sweet60"},
                    new Sweet{SweetId = 61, Name = "Sweet61"},
                    new Sweet{SweetId = 62, Name = "Sweet62"},
                    new Sweet{SweetId = 63, Name = "Sweet63"},
                    new Sweet{SweetId = 64, Name = "Sweet64"},
                    new Sweet{SweetId = 65, Name = "Sweet65"}
                });

            SweetsController controller = new SweetsController(mock.Object);
            controller.pageSize = 4;

            //Дія (act)
            SweetsListViewModel result = (SweetsListViewModel)controller.List(null,17).Model;

            //Ствердження (assert)
            List<Sweet> sweets = result.Sweets.ToList();
            Assert.IsTrue(sweets.Count == 1);
            Assert.AreEqual(sweets[0].Name, "Sweet65");
        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            // Організація
            HtmlHelper myHelper = null;
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 16,
                TotalItems = 65,
                ItemsPerPage = 4
            };
            Func<int, string> pageUrlDelegate = i => "Page" + i;

            // Дія
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            // Ствердження 
            Assert.AreEqual(
                  @"<a class=""btn btn-default"" href=""Page1"">1</a>"
                + @"<a class=""btn btn-default"" href=""Page2"">2</a>"
                + @"<a class=""btn btn-default"" href=""Page3"">3</a>"
                + @"<a class=""btn btn-default"" href=""Page4"">4</a>"
                + @"<a class=""btn btn-default"" href=""Page5"">5</a>"
                + @"<a class=""btn btn-default"" href=""Page6"">6</a>"
                + @"<a class=""btn btn-default"" href=""Page7"">7</a>"
                + @"<a class=""btn btn-default"" href=""Page8"">8</a>"
                + @"<a class=""btn btn-default"" href=""Page9"">9</a>"
                + @"<a class=""btn btn-default"" href=""Page10"">10</a>"
                + @"<a class=""btn btn-default"" href=""Page11"">11</a>"
                + @"<a class=""btn btn-default"" href=""Page12"">12</a>"
                + @"<a class=""btn btn-default"" href=""Page13"">13</a>"
                + @"<a class=""btn btn-default"" href=""Page14"">14</a>"
                + @"<a class=""btn btn-default"" href=""Page15"">15</a>"
                + @"<a class=""btn btn-default btn-primary selected"" href=""Page16"">16</a>"
                + @"<a class=""btn btn-default"" href=""Page17"">17</a>"
                ,
                result.ToString());
        }

        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            //Організація (arrange)
            Mock<ISweetRepository> mock = new Mock<ISweetRepository>();
            mock.Setup(m => m.Sweets).Returns(new List<Sweet>
                {
                    new Sweet{SweetId = 1, Name = "Sweet1"},
                    new Sweet{SweetId = 2, Name = "Sweet2"},
                    new Sweet{SweetId = 3, Name = "Sweet3"},
                    new Sweet{SweetId = 4, Name = "Sweet4"},
                    new Sweet{SweetId = 5, Name = "Sweet5"},
                    new Sweet{SweetId = 6, Name = "Sweet6"},
                    new Sweet{SweetId = 7, Name = "Sweet7"},
                    new Sweet{SweetId = 8, Name = "Sweet8"},
                    new Sweet{SweetId = 9, Name = "Sweet9"},
                    new Sweet{SweetId = 10, Name = "Sweet10"},
                    new Sweet{SweetId = 11, Name = "Sweet11"},
                    new Sweet{SweetId = 12, Name = "Sweet12"},
                    new Sweet{SweetId = 13, Name = "Sweet13"},
                    new Sweet{SweetId = 14, Name = "Sweet14"},
                    new Sweet{SweetId = 15, Name = "Sweet15"},
                    new Sweet{SweetId = 16, Name = "Sweet16"},
                    new Sweet{SweetId = 17, Name = "Sweet17"},
                    new Sweet{SweetId = 18, Name = "Sweet18"},
                    new Sweet{SweetId = 19, Name = "Sweet19"},
                    new Sweet{SweetId = 20, Name = "Sweet20"},
                    new Sweet{SweetId = 21, Name = "Sweet21"},
                    new Sweet{SweetId = 22, Name = "Sweet22"},
                    new Sweet{SweetId = 23, Name = "Sweet23"},
                    new Sweet{SweetId = 24, Name = "Sweet24"},
                    new Sweet{SweetId = 25, Name = "Sweet25"},
                    new Sweet{SweetId = 26, Name = "Sweet26"},
                    new Sweet{SweetId = 27, Name = "Sweet27"},
                    new Sweet{SweetId = 28, Name = "Sweet28"},
                    new Sweet{SweetId = 29, Name = "Sweet29"},
                    new Sweet{SweetId = 30, Name = "Sweet30"},
                    new Sweet{SweetId = 31, Name = "Sweet31"},
                    new Sweet{SweetId = 32, Name = "Sweet32"},
                    new Sweet{SweetId = 33, Name = "Sweet33"},
                    new Sweet{SweetId = 34, Name = "Sweet34"},
                    new Sweet{SweetId = 35, Name = "Sweet35"},
                    new Sweet{SweetId = 36, Name = "Sweet36"},
                    new Sweet{SweetId = 37, Name = "Sweet37"},
                    new Sweet{SweetId = 38, Name = "Sweet38"},
                    new Sweet{SweetId = 39, Name = "Sweet39"},
                    new Sweet{SweetId = 40, Name = "Sweet40"},
                    new Sweet{SweetId = 41, Name = "Sweet41"},
                    new Sweet{SweetId = 42, Name = "Sweet42"},
                    new Sweet{SweetId = 43, Name = "Sweet43"},
                    new Sweet{SweetId = 44, Name = "Sweet44"},
                    new Sweet{SweetId = 45, Name = "Sweet45"},
                    new Sweet{SweetId = 46, Name = "Sweet46"},
                    new Sweet{SweetId = 47, Name = "Sweet47"},
                    new Sweet{SweetId = 48, Name = "Sweet48"},
                    new Sweet{SweetId = 49, Name = "Sweet49"},
                    new Sweet{SweetId = 50, Name = "Sweet50"},
                    new Sweet{SweetId = 51, Name = "Sweet51"},
                    new Sweet{SweetId = 52, Name = "Sweet52"},
                    new Sweet{SweetId = 53, Name = "Sweet53"},
                    new Sweet{SweetId = 54, Name = "Sweet54"},
                    new Sweet{SweetId = 55, Name = "Sweet55"},
                    new Sweet{SweetId = 56, Name = "Sweet56"},
                    new Sweet{SweetId = 57, Name = "Sweet57"},
                    new Sweet{SweetId = 58, Name = "Sweet58"},
                    new Sweet{SweetId = 59, Name = "Sweet59"},
                    new Sweet{SweetId = 60, Name = "Sweet60"},
                    new Sweet{SweetId = 61, Name = "Sweet61"},
                    new Sweet{SweetId = 62, Name = "Sweet62"},
                    new Sweet{SweetId = 63, Name = "Sweet63"},
                    new Sweet{SweetId = 64, Name = "Sweet64"},
                    new Sweet{SweetId = 65, Name = "Sweet65"}
                });

            SweetsController controller = new SweetsController(mock.Object);
            controller.pageSize = 4;

            //Дія (act)
            SweetsListViewModel result = (SweetsListViewModel)controller.List(null,16).Model;

            PagingInfo pagingInfo = result.PagingInfo;
            Assert.AreEqual(pagingInfo.CurrentPage, 16);
            Assert.AreEqual(pagingInfo.ItemsPerPage, 4);
            Assert.AreEqual(pagingInfo.TotalItems, 65);
            Assert.AreEqual(pagingInfo.TotalPages, 17);
        }

        [TestMethod]
        public void Can_Filter_Sweets()
        {
            //Організація (arrange)
            Mock<ISweetRepository> mock = new Mock<ISweetRepository>();
            mock.Setup(m => m.Sweets).Returns(new List<Sweet>
                {
                    new Sweet{SweetId = 1, Name = "Sweet1", Type="Type1"},
                    new Sweet{SweetId = 2, Name = "Sweet2", Type="Type2"},
                    new Sweet{SweetId = 3, Name = "Sweet3", Type="Type1"},
                    new Sweet{SweetId = 4, Name = "Sweet4", Type="Type3"},
                    new Sweet{SweetId = 5, Name = "Sweet5", Type="Type2"}
                });

            SweetsController controller = new SweetsController(mock.Object);
            controller.pageSize = 3;

            //Дія (act)
            List<Sweet> result = ((SweetsListViewModel)controller.List("Type2", 1).Model).Sweets.ToList();

            Assert.AreEqual(result.Count(), 2);
            Assert.IsTrue(result[0].Name == "Sweet2" && result[0].Type == "Type2");
            Assert.IsTrue(result[1].Name == "Sweet5" && result[1].Type == "Type2");

        }

        [TestMethod]
        public void Can_Create_Categories()
        {
            //Організація (arrange)
            Mock<ISweetRepository> mock = new Mock<ISweetRepository>();
            mock.Setup(m => m.Sweets).Returns(new List<Sweet>
                {
                    new Sweet{SweetId = 1, Name = "Sweet1", Type="Type1"},
                    new Sweet{SweetId = 2, Name = "Sweet2", Type="Type2"},
                    new Sweet{SweetId = 3, Name = "Sweet3", Type="Type1"},
                    new Sweet{SweetId = 4, Name = "Sweet4", Type="Type3"},
                    new Sweet{SweetId = 5, Name = "Sweet5", Type="Type2"}
                });

            NavController target = new NavController(mock.Object);

            //Дія (act)
            List<string> result = ((IEnumerable<string>)target.Menu().Model).ToList();

            Assert.AreEqual(result.Count(), 3);
            Assert.AreEqual(result[0], "Type1");
            Assert.AreEqual(result[1], "Type2");
            Assert.AreEqual(result[2], "Type3");
        }

        [TestMethod]
        public void Indicates_Selected_Type()
        {
            //Організація (arrange)
            Mock<ISweetRepository> mock = new Mock<ISweetRepository>();
            mock.Setup(m => m.Sweets).Returns(new List<Sweet>
                {
                    new Sweet{SweetId = 1, Name = "Sweet1", Type="Type1"},
                    new Sweet{SweetId = 2, Name = "Sweet2", Type="Type2"},
                    new Sweet{SweetId = 3, Name = "Sweet3", Type="Type1"},
                    new Sweet{SweetId = 4, Name = "Sweet4", Type="Type3"},
                    new Sweet{SweetId = 5, Name = "Sweet5", Type="Type2"}
                });

            NavController target = new NavController(mock.Object);

            string typeToSelect = "Type2";

            //Дія (act)
            string result = target.Menu(typeToSelect).ViewBag.SelectedType;

            Assert.AreEqual(typeToSelect, result);
        }

        [TestMethod]
        public void Generate_Type_Specific_Sweet_Count()
        {
            //Організація (arrange)
            Mock<ISweetRepository> mock = new Mock<ISweetRepository>();
            mock.Setup(m => m.Sweets).Returns(new List<Sweet>
                {
                    new Sweet{SweetId = 1, Name = "Sweet1", Type="Type1"},
                    new Sweet{SweetId = 2, Name = "Sweet2", Type="Type2"},
                    new Sweet{SweetId = 3, Name = "Sweet3", Type="Type1"},
                    new Sweet{SweetId = 4, Name = "Sweet4", Type="Type3"},
                    new Sweet{SweetId = 5, Name = "Sweet5", Type="Type2"}
                });

            SweetsController controller = new SweetsController(mock.Object);
            controller.pageSize = 3;

            int res1 = ((SweetsListViewModel)controller.List("Type1").Model).PagingInfo.TotalItems;
            int res2 = ((SweetsListViewModel)controller.List("Type2").Model).PagingInfo.TotalItems;
            int res3 = ((SweetsListViewModel)controller.List("Type3").Model).PagingInfo.TotalItems;
            int resAll = ((SweetsListViewModel)controller.List(null).Model).PagingInfo.TotalItems;

            Assert.AreEqual(res1, 2);
            Assert.AreEqual(res1, 2);
            Assert.AreEqual(res3, 1);
            Assert.AreEqual(resAll, 5);
        }
    }
}
