using CommonInfrastructure.Extension;
using GenerateQuestsService.DataContracts.DataContracts;
using GenerateQuestsService.DataContracts.Models.Stages;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using ProcessQuestDataContracts.Models.Stages;
using ProcessQuestService.Core.HelperModels;
using System.Text;
using System.Text.Json;

namespace ProcessQuestService.Core.Helpers
{
    public class ProcessQuestCacheHelper
    {
        private IDistributedCache _cache;
        private RedisSetting _redisSetting;
        public ProcessQuestCacheHelper(IDistributedCache cache,
            IOptions<RedisSetting> redisSetting)
        {
            _cache = cache;
            _redisSetting = redisSetting.Value;
        }

        private ProcessModel GetProcessModel(string processString)
        {
            if(processString.IsNullOrEmpty())
            {
                return null;
            }
            return JsonSerializer.Deserialize<ProcessModel>(processString);
        }

        private QuestViewModel GetQuestViewModel(string questString)
        {
            if (questString.IsNullOrEmpty())
            {
                return null;
            }
            return JsonSerializer.Deserialize<QuestViewModel>(questString);
        }
        public async Task RegisterProcessingAsync(string roomKey, int userId, string questId)
        {
            string processString =  await _cache.GetStringAsync(roomKey);
            //новую комнату раньше создавали, но не сохраняли в кеш
            var process = GetProcessModel(processString) ?? new ProcessModel()
            {
                Key = roomKey,
                QuestId = questId
            };
            //повторное подключение того же пользователя не должно падать на Add
            process.UserProcessing[userId] = 0;
            await _cache.SetStringAsync(roomKey, JsonSerializer.Serialize(process));
        }
        private async Task AddRoomAsync(string roomKey)
        {
            string rooms = await _cache.GetStringAsync(_redisSetting.RoomKey);
            if (rooms.IsNullOrEmpty())
            {
                await _cache.SetStringAsync(_redisSetting.RoomKey, roomKey);
            }
            else
            {
                rooms += ',' + roomKey;
                await _cache.SetStringAsync(_redisSetting.RoomKey, rooms);
            }
        }

        public async Task<bool> IsSetRoomKeyAsync(string roomKey)
        {
            //если не пришел ключ - значит такая комната есть, условно
            if (!string.IsNullOrEmpty(roomKey))
            {
                //если есть ключи комнат и такой уже есть - возвращаем тру
                string rooms = await _cache.GetStringAsync(_redisSetting.RoomKey);

                //сравниваем ключи целиком: Contains по строке находил "12" внутри "1234"
                if(!rooms.IsNullOrEmpty() && rooms.Split(',').Contains(roomKey)) {
                    return false;
                }
                await AddRoomAsync(roomKey);
                return true;
            }
            return false;
        }

        public async Task<bool> SetQuestAsync(QuestViewModel quest)
        {
            string questId = quest != null ? quest.Id.ToString() : null;
            if (questId == null)
            {
                return false;
            }
            //перезаписываем квест при каждом подключении: так обновляются и данные, и срок хранения.
            //Раньше вызывался RefreshAsync по ключу, равному JSON квеста (а не его id), без await,
            //и он не продлевает абсолютный срок жизни записи.
            await _cache.SetStringAsync(questId,
                JsonSerializer.Serialize(quest),
                new DistributedCacheEntryOptions()
            {
                  AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            });
            return true;
        }

        public async Task<QuestViewModel> GetQuestAsync(string room)
        {
            string roomString = await _cache.GetStringAsync(room);
            var process = GetProcessModel(roomString);
            return process == null ? null : 
                GetQuestViewModel(await _cache.GetStringAsync(process.QuestId));
        }

        public bool IsQuestReady(int id)
        {
            return false;
        }
    }
}
