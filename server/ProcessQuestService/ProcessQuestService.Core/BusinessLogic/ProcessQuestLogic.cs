using GenerateQuestsService.DataContracts.JsonHelpers;
using GenerateQuestsService.DataContracts.Models.Stages;
using ProcessQuestService.Core.Helpers;
using ProcessQuestService.Core.InteractionWebSocketModel;
using System.Text;
using System.Text.Json;

namespace ProcessQuestService.Core.BusinessLogic
{
    public class ProcessQuestLogic
    {
        private JsonSerializerOptions _jsonSerializerOptions;
        private ProcessQuestCacheHelper _cacheHelper;

        public ProcessQuestLogic(ProcessQuestCacheHelper cacheHelper) {
            _jsonSerializerOptions = new JsonSerializerOptions().SetQuestJsonSerializerOptions();
            _cacheHelper = cacheHelper;
        }

        private WebSocketRequest? Deserialize(byte[] buffer, int length)
        {
            //декодируем ровно полученные байты: length - число байт, а не символов
            string requestString = Encoding.UTF8.GetString(buffer, 0, length);
            return JsonSerializer.Deserialize<WebSocketRequest>(requestString, _jsonSerializerOptions);
        }

        public async Task<WebSocketResponse> ProcessAsync(byte[] buffer, int length, string room)
        {
            try
            {
                var request = Deserialize(buffer, length);
                if (request?.Stage == null)
                {
                    return BuildWebSocketResponseHelper.BuildErrorModelResponse();
                }
                var quest = await _cacheHelper.GetQuestAsync(room);
                if(quest == null)
                {
                    return BuildWebSocketResponseHelper.BuildErrorResponse("Нет такого квеста");
                }
                var questStage = quest.Stages?.FirstOrDefault(el => el.Equals(request.Stage));

                if(questStage == null)
                {
                    return BuildWebSocketResponseHelper.BuildErrorResponse("Нет такого этапа");
                }
                if(StageProcessing(request.Stage, questStage))
                {
                    //TODO: выдача следующего этапа (WebSocketResponse.Stage) ещё не реализована
                    return new WebSocketResponse { Sucsess = true };
                }
                return BuildWebSocketResponseHelper.BuildErrorChoiseResponse();

            }
            catch (JsonException)
            {
                return BuildWebSocketResponseHelper.BuildErrorModelResponse();
            }
            catch (Exception)
            {
                //текст исключения клиенту не отдаём, чтобы не раскрывать внутренности сервиса
                return BuildWebSocketResponseHelper.BuildErrorResponse("Внутренняя ошибка сервиса");
            }
        }

        private static bool StageProcessing(Stage userStage, Stage questStage)
        {
            //TODO: проверка ответа пользователя по типу этапа (карта, QR-код, тест, текст, видео)
            //ещё не реализована, поэтому любой ответ пока считается неверным.
            return false;
        }
    }
}
