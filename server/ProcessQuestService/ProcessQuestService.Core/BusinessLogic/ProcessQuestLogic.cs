﻿using AutoMapper;
using CommonInfrastructure.Extension;
using CommonInfrastructure.Http;
using CommonInfrastructure.Http.Helpers;
using GenerateQuestsService.DataContracts.DataContracts;
using GenerateQuestsService.DataContracts.Interfaces;
using ProcessQuestDataContracts.DataContracts;
using ProcessQuestDataContracts.ViewModels;
using ProcessQuestService.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProcessQuestService.Core.BusinessLogic
{
    public class ProcessQuestLogic
    {
        private IGenerateQuestsApi _generateQuestsApi;
        private IMapper _mapper;
        private Random _random;
        private ProcessQuestCacheHelper _cacheHelper;

        public ProcessQuestLogic(IGenerateQuestsApi generateQuestsApi,
            IMapper mapper,
            Random random,
            ProcessQuestCacheHelper cacheHelper)
        {
            _generateQuestsApi = generateQuestsApi;
            _mapper = mapper;
            _random = random;
            _cacheHelper = cacheHelper;
        }

        private async Task<string> GenerateNumberRoom()
        {
            int attempt = 0;
            while (attempt < 10) {
                string value = _random.Next(int.MaxValue).ToString();

                if (await _cacheHelper.IsSetRoomKey(value))
                {
                    return value;
                }
                attempt++;
            }
            return null;
        }
        public async Task<CommonHttpResponse<StartQuestViewModel>> ConnectToQuestAsync(StartQuestContract contract)
        {
            try
            {
                var questRes = await _generateQuestsApi.GetQuestAsync(
                    _mapper.Map<GetQuestContract>(contract)
                );
                if (!questRes.Success)
                {
                    return CommonHttpHelper.BuildErrorResponse<StartQuestViewModel>(
                        extErrors: questRes.Errors.ToList());
                }
                //если политика квеста публичная и не групповая
                if (true)
                {
                    //генерируем номер комнаты
                    string roomKey = await GenerateNumberRoom();

                    if(roomKey.IsNullOrEmpty()) {
                        return CommonHttpHelper.BuildErrorResponse<StartQuestViewModel>(
                        initialError: "Не удалось создать комнату прохождения");
                    }
                    
                    //устанавливаем квест в кеш на время прохождения квеста
                    await _cacheHelper.SetQuest(questRes.Data);
                    
                    //регистрируем прохождение
                    if(contract.RequestUserId.HasValue)
                    {
                        await _cacheHelper.RegisterProcessing(roomKey, contract.RequestUserId.Value);
                    }
                    else
                    {
                        return CommonHttpHelper.BuildErrorResponse<StartQuestViewModel>(
                            initialError: "Нет пользователя в запросе", statusCode: HttpStatusCode.Unauthorized);
                    }

                    var result = new StartQuestViewModel()
                    {
                        Room = roomKey,
                        Quest = _mapper.Map<QuestProcessViewModel>(questRes.Data)
                    };

                    return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
                }
                else
                {
                    //проверяем запущен ли квест
                }
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<StartQuestViewModel>(
                   HttpStatusCode.InternalServerError,
                   ex.ToExceptionDetails(),
                   $"Ошибка выполнения метода {nameof(ConnectToQuestAsync)} ReqId : {contract.RequestId}");

            }
        }
    }
}
