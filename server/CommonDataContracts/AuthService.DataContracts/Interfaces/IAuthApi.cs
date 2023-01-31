using AuthService.DataContracts.User;
using CommonInfrastructure.Http;
using Refit;
using System.Net.Http;

namespace AuthService.DataContracts.Interfaces
{
    public interface IAuthApi
    {
        #region Auth
        [Post("/Auth/Register")]
        Task<CommonHttpResponse<UserViewModel>> RegisterAsync([Body]CreateUserContract createContract);

        [Post("/Auth/GetUserInfo")]
        Task<CommonHttpResponse<UserViewModel>> GetUserInfoAsync([Body] LoginContract contract);

        [Post("/Auth/Login")]
        Task<CommonHttpResponse<UserViewModel>> LoginAsync([Body]LoginContract contract);

        #endregion
    }
}
