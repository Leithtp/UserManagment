using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagment.Domain.Entities;

namespace UserManagment.Domain.Repositories.Abstract
{
    public interface IUserDataRepository
    {
        IQueryable<UserData> GetUserData();
        UserData GetUserDataById(string id);
        void SaveUserData(UserData entity);
        void DeleteUserData(string id);

    }
}
