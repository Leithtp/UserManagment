using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagment.Domain.Entities
{
    public class UserData : IdentityUser
    {

        [Display (Name = "Фамилия")]
        public string Surname { get; set; }

        [Display (Name = "Имя")]
        public string Name { get; set; }

        [Display (Name = "Отчество")]
        public string Patronymic { get; set; }
        [NotMapped]
        public bool IsAdmin { get; set; }

        public string FullName { get { return Surname + " " + Name + " " + Patronymic; } }

        
        

    }
}
