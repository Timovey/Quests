using CommonInfrastructure.Extension;
using GenerateQuestsService.DataContracts.DataContracts;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using ProcessQuestService.Core.HelperModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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

        public async Task RegisterProcessing(string roomKey, int userId)
        {
            string processString =  await _cache.GetStringAsync(roomKey);
            if(processString.IsNullOrEmpty()) {
                var process = new ProcessModel()
                {
                    Key = roomKey,
                };
                process.UserProcessing.Add(userId, 0);
            }
            else
            {
                var process = JsonSerializer.Deserialize<ProcessModel>(processString);
                process.UserProcessing.Add(userId, 0);
                await _cache.SetStringAsync(roomKey, JsonSerializer.Serialize(process));
            }
        }
        private async Task AddRoom(string roomKey)
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


        public async Task<bool> IsSetRoomKey(string roomKey)
        {
            //если не пришел ключ - значит такая комната есть, условно
            if (!string.IsNullOrEmpty(roomKey))
            {
                //если есть ключи комнат и такой уже есть - возвращаем тру
                string rooms = await _cache.GetStringAsync(_redisSetting.RoomKey);

                if(!rooms.IsNullOrEmpty() && rooms.Contains(roomKey)) {
                    return false;
                }
                await AddRoom(roomKey);
                return true;
            }
            return false;
        }

        public async Task<bool> SetQuest(QuestViewModel quest)
        {
            if (quest == null)
            {
                return false;
            }
            else if(await _cache.GetStringAsync(quest.Id.ToString()) != null)
            {
                //делаем для того, чтобы обновилось время хранения квеста
                _cache.RefreshAsync(_cache.GetString(quest.Id.ToString()));
                return true;
            }
            else
            {
                //кешируем в зависимости от длительности квеста
                await _cache.SetStringAsync(quest.Id.ToString(), 
                    JsonSerializer.Serialize(quest),
                    new DistributedCacheEntryOptions()
                {
                      AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
                });
                return true;
            }
        }

        public async Task<QuestViewModel> GetQuest(int id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                string questString = await _cache.GetStringAsync(id.ToString());
                if (questString != null)
                {
                    return JsonSerializer.Deserialize<QuestViewModel>(questString);
                }
            }
            return null;
        }

        public bool IsQuestReady(int id)
        {
            return false;
        }
    }
}
