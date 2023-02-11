using CommonInfrastructure.Http;
using GenerateQuestsService.DataContracts.DataContracts;
using Refit;

namespace GenerateQuestsService.DataContracts.Interfaces
{
    public interface IGenerateQuestsApi
    {
        [Post("/GenerateQuests/CreateQuest")]
          Task<CommonHttpResponse> CreateQuestAsync(
          [Body] CreateQuestContract contract);

        [Put("/GenerateQuests/UpdateQuest")]
        Task<CommonHttpResponse<bool>> UpdateQuestAsync(
          [Body] UpdateQuestContract contract);

        [Post("/GenerateQuests/GetQuest")]
        Task<CommonHttpResponse<QuestViewModel>> GetQuestAsync(
          [Body] GetQuestContract contract);

        [Post("/GenerateQuests/GetFilteredQuests")]
        Task<CommonHttpResponse<IList<ShortQuestViewModel>>> GetFilteredQuestsAsync(
          [Body] GetFilteredQuestsContract contract);

        [Delete("/GenerateQuests/DeleteQuest/{id}")]
        Task<CommonHttpResponse<bool>> DeleteQuestAsync(
          [Body] DeleteQuestContract contract);

    }
}
