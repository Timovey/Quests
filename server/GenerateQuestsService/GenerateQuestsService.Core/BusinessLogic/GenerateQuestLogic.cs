using AuthService.DataContracts.CommonContracts;
using AuthService.DataContracts.Interfaces;
using AutoMapper;
using CommonDatabase.QuestDatabase.Interfaces;
using CommonInfrastructure.Extension;
using CommonInfrastructure.Http;
using CommonInfrastructure.Http.Helpers;
using GenerateQuestsService.DataContracts.DataContracts;
using System.Diagnostics.Contracts;
using System.Net;


namespace GenerateQuestsService.Core.BusinessLogic
{
    public class GenerateQuestLogic
    {
        private IGenerateQuestStorage _generateQuestStorage;
        private IMapper _mapper;
        private IAuthApi _authApi;

        public GenerateQuestLogic(IGenerateQuestStorage generateQuestStorage, IMapper mapper, IAuthApi authApi)
        {
            _generateQuestStorage = generateQuestStorage;
            _mapper = mapper;
            _authApi = authApi;
        }

        public async Task<CommonHttpResponse> CreateQuestAsync(CreateQuestContract quest)
        {
            if(quest == null)
            {
                return CommonHttpHelper.BuildErrorResponse(initialError: "Пустой запрос");
            }
            if (quest.RequestUserId == null)
            {
                return CommonHttpHelper.BuildErrorResponse<QuestViewModel>(initialError: "Нет автора квеста");
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
                    $"Ошибка выполнения метода {nameof(CreateQuestAsync)} ReqId : ");
            }
        }

        public async Task<CommonHttpResponse<QuestViewModel>> GetQuestAsync(GetQuestContract contract)
        {
            if (contract == null)
            {
                return CommonHttpHelper.BuildErrorResponse< QuestViewModel>(initialError: "Пустой запрос");
            }
            try
            {
                var result = await _generateQuestStorage.GetQuestAsync(contract);
                if(result == null)
                {
                    return CommonHttpHelper.BuildErrorResponse<QuestViewModel>(initialError: "Нет такого квеста");
                }
                var authorRes = await _authApi.GetUserByIdAsync(new GetContract
                {
                    Id = result.UserId
                });
                if(!authorRes.Success || (authorRes.Success && authorRes.Data.UserName.IsNullOrEmpty()))
                {
                    result.Author = "";
                }
                else
                {
                    result.Author = authorRes.Data.UserName;
                }
                return CommonHttpHelper.BuildSuccessResponse(_mapper.Map<QuestViewModel>(result), HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<QuestViewModel>(
                    HttpStatusCode.InternalServerError,
                                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(GetQuestAsync)} ReqId : ");
            }
        }

    }
}
