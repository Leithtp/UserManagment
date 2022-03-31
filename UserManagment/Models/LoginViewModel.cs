using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace UserManagment.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display (Name = "Логин")]
        public string UserName { get; set; }

        [UIHint("password")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display (Name = "Запомнить меня?")]
        public bool RememberMe { get; set; } 
    }
}
