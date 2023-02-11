using AuthService.DataContracts.CommonContracts;
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

        [Get("/Auth/GetUserById/{id}")]
        Task<CommonHttpResponse<ShortUserViewModel>> GetUserByIdAsync([AliasAs("id")] int id);

        [Post("/Auth/GetFilteredUsers")]
        public Task<CommonHttpResponse<IList<ShortUserViewModel>>> GetFilteredUsersAsync([Body] GetFilteredUsersContract contract);

        #endregion
    }
}
