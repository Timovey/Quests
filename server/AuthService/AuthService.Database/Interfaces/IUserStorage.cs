using AuthService.DataContracts.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Database.Interfaces
{
    public interface IUserStorage
    {
        public UserViewModel AddUser(CreateUserContract createContract);

        public bool IsUserUnique(CreateUserContract createContract);
    }
}
