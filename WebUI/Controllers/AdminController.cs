using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        ISweetRepository repository;

        public AdminController(ISweetRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.Sweets);
        }

        public ViewResult Edit(int sweetId)
        {
            Sweet sweet = repository.Sweets.FirstOrDefault(s => s.SweetId == sweetId);

            return View(sweet);
        }

        [HttpPost]
        public ActionResult Edit(Sweet sweet, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    sweet.ImageMimeType = image.ContentType;
                    sweet.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(sweet.ImageData, 0, image.ContentLength);
                }
                repository.SaveSweet(sweet);
                TempData["message"] = string.Format("Зміни інформації про товар \"{0}\" збережено", sweet.Name);
                return RedirectToAction("Index");
            }
            else
            {
                //Проблеми з значеннями даних
                return View(sweet);
            }
        }
    }
}