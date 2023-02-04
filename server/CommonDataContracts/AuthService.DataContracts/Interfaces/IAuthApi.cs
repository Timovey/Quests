using AuthService.DataContracts.User;
using CommonInfrastructure.Http;
using Refit;

namespace AuthService.DataContracts.Interfaces
{
    public interface IAuthApi
    {
        #region Auth
        [Post("/Auth/Register")]
        Task<CommonHttpResponse<UserViewModel>> RegisterAsync([Body]CreateUserContract createContract);

        [Post("/Auth/GetUserInfo")]
        Task<CommonHttpResponse<UserViewModel>> GetUserInfoAsync([Body] LoginTokenContract contract);

        [Post("/Auth/Login")]
        Task<CommonHttpResponse<UserViewModel>> LoginAsync([Body] LoginContract contract);

        [Post("/Auth/LoginByRefresh")]
        Task<CommonHttpResponse<UserViewModel>> LoginByRefreshAsync([Body] LoginTokenContract contract);

        #endregion
    }
}
