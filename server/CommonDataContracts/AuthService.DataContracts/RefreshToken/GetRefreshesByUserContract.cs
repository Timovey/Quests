using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.DataContracts.RefreshToken
{
    public class GetRefreshesByUserContract
    {
        public int UserId { get; set; }
    }
}
