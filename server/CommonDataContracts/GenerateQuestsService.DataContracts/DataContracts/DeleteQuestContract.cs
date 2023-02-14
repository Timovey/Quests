using CommonInfrastructure.Http;
using Microsoft.AspNetCore.Mvc;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GenerateQuestsService.DataContracts.DataContracts
{
    public class DeleteQuestContract : CommonHttpRequest
    {
        [FromRoute(Name = "id")]
        [AliasAs("id")]
        //[JsonPropertyName("id")]
        public int Id { get; set; }
    }
}
