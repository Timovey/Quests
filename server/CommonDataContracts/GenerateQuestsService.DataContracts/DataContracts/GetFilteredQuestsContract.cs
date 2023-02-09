using CommonInfrastructure.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateQuestsService.DataContracts.DataContracts
{
    public class GetFilteredQuestsContract : CommonHttpRequest
    {

        public int? Page { get; set; }

        public int? UserId { get; set; }

        public int? Count { get; set; }

        public bool? IsFilterByUser { get; set; }
    }
}
