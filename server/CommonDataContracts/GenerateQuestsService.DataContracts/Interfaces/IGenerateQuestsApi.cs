using CommonInfrastructure.Http;
using GenerateQuestsService.DataContracts.DataContracts;
using Refit;

namespace GenerateQuestsService.DataContracts.Interfaces
{
    public interface IGenerateQuestsApi
    {
        [Post("/GenerateQuests/CreateQuest")]
          Task<CommonHttpResponse> CreateQuestAsync(
          [Body] CreateQuestContract contract
       );
    }
}
