using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Будь-ласка, введіть ім'я користувача")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Будь-ласка, введіть пароль")]
        public string Password { get; set; }
    }
}