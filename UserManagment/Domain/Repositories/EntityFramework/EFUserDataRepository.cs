using System;
using System.Linq;
using UserManagment.Domain.Entities;
using UserManagment.Domain.Repositories.Abstract;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace UserManagment.Domain.Repositories.EntityFramework
{
    public class EFUserDataRepository : IUserDataRepository 
    {
        private readonly AppDbContext context;
        private readonly UserManager<UserData> _userManager;

        public EFUserDataRepository(AppDbContext context, UserManager<UserData> userManager)
        {
            this.context = context;
            _userManager = userManager;
        }


        public IQueryable<UserData> GetUserData()
        {
            foreach(UserData user in context.Users)
            {
                IList<string> roles =  _userManager.GetRolesAsync(user).Result;

                for (int i = 0; i < roles.Count; i++)
                {
                    if (roles[i] == "admin")
                        user.IsAdmin = true;
                }
            }
            return context.Users;
        }

        public UserData GetUserDataById(string id)
        {
            return context.Users.FirstOrDefault(x => x.Id == id);
        }

        public async void SaveUserData(UserData entity)
        {
            var user = context.Users.FirstOrDefault(u => u.Id == entity.Id);
            if (user == null)
            {
                UserData _user = new UserData() { Email = entity.Email, Name = entity.Name, UserName = entity.Email, Surname = entity.Surname, Patronymic = entity.Patronymic, Id = entity.Id, NormalizedEmail = entity.Email.ToUpper(), NormalizedUserName = entity.Email.ToUpper() };
                context.Users.Add(_user);
                context.SaveChanges();

                user = context.Users.FirstOrDefault(u => u.Id == entity.Id);
                
                
            }
 
            

            user.Email = entity.Email;
            user.Name = entity.Name;
            user.Surname = entity.Surname;
            user.Patronymic = entity.Patronymic;
            user.UserName = entity.Email;
            user.NormalizedEmail = entity.Email.ToUpper();
            user.NormalizedUserName = entity.Email.ToUpper();

            context.Users.Update(user);
            context.SaveChanges();



        }

        public void DeleteUserData(string id)
        {
            var user = context.Users.FirstOrDefault(u => u.Id == id);
            context.Users.Remove(user);
            context.SaveChanges();

        }

    }
}
