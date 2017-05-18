using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ShippingDetails
    {
        [Required(ErrorMessage = "Вкажіть Ваше ім'я")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Вкажіть адресу доставки")]
        [Display(Name = "Перша адреса")]
        public string Line1 { get; set; }
        [Display(Name = "Друга адреса")]
        public string Line2 { get; set; }
        [Display(Name = "Третя адреса")]
        public string Line3 { get; set; }

        [Required(ErrorMessage = "Вкажіть місто")]
        [Display(Name = "Місто")]
        public string City { get; set; }

        [Required(ErrorMessage = "Вкажіть країну")]
        [Display(Name = "Країна")]
        public string Country { get; set; }

        public bool GiftWrap { get; set; }
    }
}
