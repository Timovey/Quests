using CommonInfrastructure.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateQuestsService.DataContracts.DataContracts
{
    public class GetQuestContract : CommonHttpRequest
    {
        public int Id { get; set; }
    }
}
