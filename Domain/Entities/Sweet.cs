using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Domain.Entities
{
    public class Sweet
    {
        [HiddenInput(DisplayValue=false)]
        [Display(Name = "ID")]
        public int SweetId { get; set; }

        [Display(Name="Назва")]
        [Required(ErrorMessage="Будь-ласка, вкажіть назву товару")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Будь-ласка, введіть склад товару")]
        [Display(Name="Склад")]
        public string Ingredients { get; set; }

        [Display(Name = "Фасування (кг)")]
        [Required(ErrorMessage = "Будь-ласка, вкажіть фасування товару")]
        public int Packing { get; set; }

        [Display(Name = "Термін придатності до споживання")]
        [Required(ErrorMessage = "Будь-ласка, введіть термін придатності товару")]
        public string Expiration_date { get; set; }

        [Display(Name = "Вид продукції")]
        [Required(ErrorMessage = "Будь-ласка, вкажіть вид товару")]
        public string Type { get; set; }

        [Display(Name = "Ціна (грн)")]
        [Required]
        [Range(0.01,double.MaxValue, ErrorMessage = "Будь-ласка, введіть додатнє значення ціни товару")]
        public decimal Price { get; set; }

        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
    }
}
