using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagment.Domain.Repositories.Abstract;

namespace UserManagment.Domain
{
    public class DataManager
    {
        public ITextFieldsRepository TextFields { get; set; }
        public IUserDataRepository UsersData { get; set; }
        
        public DataManager(ITextFieldsRepository textFieldsRepository, IUserDataRepository userDataRepository)
        {
            TextFields = textFieldsRepository;
            UsersData = userDataRepository;
        }
    
    }
}
