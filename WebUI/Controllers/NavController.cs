using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class NavController : Controller
    {
        private ISweetRepository repository;

        public NavController(ISweetRepository repo)
        {
            repository = repo;
        }

        public PartialViewResult Menu(string type = null)
        {
            ViewBag.SelectedType = type;

            IEnumerable<string> types = repository.Sweets
                .Select(sweet => sweet.Type)
                .Distinct()
                .OrderBy(x => x);
            return PartialView("FlexMenu", types);
        }

        public PartialViewResult Filtering(string name = null)
        {
            ViewBag.SelectedName = name;

            IEnumerable<string> names = repository.Sweets
                .Select(sweet => sweet.Name)
                .Distinct()
                .OrderBy(x => x);
            return PartialView("FlexMenu", names);
        }
    }
}