using CommonDatabase.QuestDatabase.Interfaces;
using CommonInfrastructure.Extension;
using CommonInfrastructure.Http;
using CommonInfrastructure.Http.Helpers;
using GenerateQuestsService.DataContracts.Models;
using System.Net;


namespace GenerateQuestsService.Core.BusinessLogic
{
    public class GenerateQuestLogic
    {
        private IGenerateQuestStorage _generateQuestStorage;
        public GenerateQuestLogic(IGenerateQuestStorage generateQuestStorage)
        {
            _generateQuestStorage = generateQuestStorage;
        }

        public async Task<CommonHttpResponse> CreateQuestAsync(Quest quest)
        {
            if(quest == null)
            {
                return CommonHttpHelper.BuildErrorResponse(initialError: "Пустой запрос");
            }
            try
            {
                await _generateQuestStorage.CreateQuestAsync(quest);
                return CommonHttpHelper.BuildSuccessResponse(HttpStatusCode.OK);
            }
            catch(Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse(
                    HttpStatusCode.InternalServerError,
                                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(CreateQuestAsync)} ReqId : {quest.RequestId}");
            }
        }

    }
}
