using CommonInfrastructure.Http;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateQuestsService.DataContracts.DataContracts
{
    public class DeleteQuestContract : CommonHttpRequest
    {
        [AliasAs("id")]
        public int Id { get; set; }
    }
}
