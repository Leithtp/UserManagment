using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace UserManagment.Domain.Entities
{
    public abstract class EntityBase
    {
        [Required]
        public Guid id { get; set; }

        [Display (Name = "Название (заголовок)")]
        public virtual string Title { get; set; }

        [Display (Name = "Фамилия")]
        public virtual string Surname { get; set; }

        [Display (Name = "Имя")]
        public virtual string Name { get; set; }

        [Display (Name = "Отчество")]
        public virtual string Patronymic { get; set; }

        [Display (Name = "Email")]
        public virtual string Email { get; set; }


    }
}
