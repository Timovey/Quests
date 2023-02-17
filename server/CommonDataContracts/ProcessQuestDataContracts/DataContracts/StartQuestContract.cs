using CommonInfrastructure.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessQuestDataContracts.DataContracts
{
    public class StartQuestContract : CommonHttpRequest
    {
        public int Id { get; set; }

    }
}
