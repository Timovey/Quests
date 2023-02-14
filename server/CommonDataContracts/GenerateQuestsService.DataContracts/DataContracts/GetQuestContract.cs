using CommonInfrastructure.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateQuestsService.DataContracts.DataContracts
{
    public class GetQuestContract : CommonHttpRequest
    {
        [FromRoute(Name = "id")]
        public int Id { get; set; }
    }
}
