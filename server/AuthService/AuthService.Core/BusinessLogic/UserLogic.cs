using AuthService.Database.Interfaces;
using AuthService.DataContracts.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Core.BusinessLogic
{
    public class UserLogic
    {
        private IUserStorage _userStorage;
        public UserLogic(IUserStorage userStorage)
        {                              
            _userStorage = userStorage;
        }
        public Task<UserViewModel> Register(CreateUserContract createContract)
        {
            if(!_userStorage.IsUserUnique(createContract))
            {
                return
            }
            
        }
    }
}
