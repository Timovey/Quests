using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.DataContracts.Common
{
    public class CommonHttpResponse
    {
        public bool Success { get; set; }

        public string[] Errors { get; set; }
    }
    public class CommonHttpResponse<T> : CommonHttpResponse
    {
        public T Data { get; set; }
    }
}
