using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class GlobalController : Controller
    {
        public ActionResult Main()
        {
            return View();
        }

        public ActionResult Contacts()
        {
            return View();
        }

        public ActionResult AboutUs()
        {
            return View();
        }
    }
}