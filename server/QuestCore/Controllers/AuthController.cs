using AuthService.DataContracts.Interfaces;
using AuthService.DataContracts.User;
using CommonInfrastructure.Http;
using Microsoft.AspNetCore.Mvc;

namespace QuestCore.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController : Controller
    {
        private IAuthApi _authApi;
        public AuthController(IAuthApi authApi)
        {              
            _authApi = authApi;
        }

        [HttpPost]
        public Task<CommonHttpResponse<UserViewModel>> Register(CreateUserContract createContract)
        {
            return _authApi.RegisterAsync(createContract);
        }

        [HttpPost]
        public Task<CommonHttpResponse<UserViewModel>> Login(LoginContract contract)
        {
            return _authApi.LoginAsync(contract);
        }

        [HttpPost]
        public Task<CommonHttpResponse<UserViewModel>> GetUserInfo(LoginTokenContract contract)
        {
           return _authApi.GetUserInfoAsync(contract);
        }

        [HttpPost]
        public Task<CommonHttpResponse<UserViewModel>> LoginByRefresh(LoginTokenContract contract)
        {
            return _authApi.LoginByRefreshAsync(new LoginTokenContract()
            {
                Token = contract.Token,
                RefreshToken = contract.RefreshToken,
            });
        }

        [HttpPost]
        public Task FF(CommonHttpRequest req)
        {
            return Task.CompletedTask;
        }
    }
}
