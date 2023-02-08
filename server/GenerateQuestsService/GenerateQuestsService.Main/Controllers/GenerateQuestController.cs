using CommonInfrastructure.Http;
using GenerateQuestsService.Core.BusinessLogic;
using GenerateQuestsService.DataContracts.DataContracts;
using GenerateQuestsService.DataContracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        public Task<CommonHttpResponse> CreateQuestAsync(CreateQuestContract quest)
        {
            return _generateQuestLogic.CreateQuestAsync(quest);
        }

        [HttpPost]
        public Task<CommonHttpResponse> TestAsync(SimpleContract contract)
        {
            return _generateQuestLogic.CreateQuestAsync(null);
        }
    }
}
