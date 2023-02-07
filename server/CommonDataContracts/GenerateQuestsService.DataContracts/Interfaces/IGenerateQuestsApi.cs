using CommonInfrastructure.Http;
using GenerateQuestsService.DataContracts.Models;
using Refit;

namespace GenerateQuestsService.DataContracts.Interfaces
{
    public interface IGenerateQuestsApi
    {
        [Post("/GenerateQuests/CreateQuest")]
          Task<CommonHttpResponse> CreateQuestAsync(
          [Body] Quest quest
       );
    }
}
