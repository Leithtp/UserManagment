using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagment.Models.ViewComponents
{
    public class PasswordViewModel
    {
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string UserId { get; set; }
        public string Code { get; set; }

    }
}
