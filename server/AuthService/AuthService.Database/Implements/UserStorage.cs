using AuthService.Database.Interfaces;
using AuthService.DataContracts.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Database.Implements
{
    public class UserStorage : IUserStorage
    {
        public UserViewModel AddUser(CreateUserContract createContract)
        {
            throw new NotImplementedException();
        }
    }
}
