using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class SweetsListViewModel
    {
        public IEnumerable<Sweet> Sweets { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentType { get; set; }
        public string CurrentOrderBy { get; set; }
    }
}