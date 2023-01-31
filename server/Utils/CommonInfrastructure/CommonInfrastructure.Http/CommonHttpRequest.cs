using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonInfrastructure.Http
{
    /// <summary>
    /// Общий вид HTTP запроса
    /// </summary>
    public class CommonHttpRequest
    {
        public Guid? RequestId { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }
    }

}
