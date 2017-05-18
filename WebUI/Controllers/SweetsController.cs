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
    public class SweetsController : Controller
    {
        private ISweetRepository repository;
        public int pageSize = 4;

        public SweetsController(ISweetRepository repo)
        {
            repository = repo;
        }

        public ViewResult List( string type, int page = 1)
        {
            SweetsListViewModel model = new SweetsListViewModel
            {
                Sweets = repository.Sweets
                .Where(s => type == null || s.Type == type)
                .OrderBy(sweet => sweet.SweetId)
                .Skip((page - 1)*pageSize)
                .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = type == null ?
                    repository.Sweets.Count() : 
                    repository.Sweets.Where(sweet => sweet.Type == type).Count()
                },
                CurrentType = type
            };

            return View(model);
        }

        public FileContentResult GetImage(int sweetId)
        {
            Sweet sweet = repository.Sweets
                .FirstOrDefault(s => s.SweetId == sweetId);

            if (sweet != null)
            {
                return File(sweet.ImageData, sweet.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}