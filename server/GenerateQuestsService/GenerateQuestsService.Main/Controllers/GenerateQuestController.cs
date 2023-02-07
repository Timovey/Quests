using CommonInfrastructure.Http;
using GenerateQuestsService.Core.BusinessLogic;
using GenerateQuestsService.DataContracts.Interfaces;
using GenerateQuestsService.DataContracts.Models;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace GenerateQuestsService.Main.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class GenerateQuestController : Controller, IGenerateQuestsApi
    {
        private readonly GenerateQuestLogic _generateQuestLogic;
        public GenerateQuestController(GenerateQuestLogic generateQuestLogic)
        {
            _generateQuestLogic = generateQuestLogic;
        }

        [HttpPost]
        public Task<CommonHttpResponse> CreateQuestAsync(Quest quest)
        {
            return _generateQuestLogic.CreateQuestAsync(quest);
        }
    }
}
