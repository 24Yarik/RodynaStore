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
    [Route("[controller]/[action]")]
    public class SweetsController : Controller
    {
        private ISweetRepository repository;
        public int pageSize = 4;

        public SweetsController(ISweetRepository repo)
        {
            repository = repo;
        }

        [HttpGet]
        public ViewResult List(string type, string orderBy, bool? byAsc, int page = 1)
        {
            type = String.IsNullOrEmpty(type) ? null : type;

            var sweets = String.IsNullOrEmpty(type) ? 
                repository.Sweets :
                repository.Sweets.Where(s => s.Type == null || s.Type == type);

            switch (orderBy)
            {
                case "name": sweets = !byAsc.HasValue || byAsc.Value.Equals(true) ? sweets.OrderBy(x => x.Name) : sweets.OrderByDescending(x => x.Name); break;
                case "price": sweets = !byAsc.HasValue || byAsc.Value.Equals(true) ? sweets.OrderBy(x => x.Price) : sweets.OrderByDescending(x => x.Price); break;
                default: sweets = sweets.OrderBy(x => x.SweetId); break;
            }

            SweetsListViewModel model = new SweetsListViewModel
            {
                Sweets = sweets.Skip((page - 1)*pageSize).Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = type == null ?
                    repository.Sweets.Count() : 
                    repository.Sweets.Where(sweet => sweet.Type == type).Count()
                },
                CurrentType = type,
                CurrentOrderBy = orderBy,
                CurrentByAsc = byAsc,
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