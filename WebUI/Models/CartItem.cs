using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class CartItem
    {
        public int CartId { get; set; }
        public int GoodId { get; set; }
        public int Quantity { get; set; }
    }
}