using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.DataContracts.User
{
    public class GetFilteredUsersContract
    {
        public IList<int> Ids { get; set; }
    }
}
