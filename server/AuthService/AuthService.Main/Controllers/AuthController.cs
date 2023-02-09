using AuthService.Core.BusinessLogic;
using AuthService.DataContracts.CommonContracts;
using AuthService.DataContracts.Interfaces;
using AuthService.DataContracts.User;
using CommonInfrastructure.Http;
using CommonInfrastructure.Http.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Main.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [AddUserInRequest]
    public class AuthController : Controller, IAuthApi
    {
        private UserLogic _userLogic;
        public AuthController(UserLogic userLogic)
        {                         
            _userLogic = userLogic;
        }

        [HttpPost]
        public Task<CommonHttpResponse<UserViewModel>> RegisterAsync(CreateUserContract createContract)
        {
            return _userLogic.RegisterAsync(createContract);
        }

        [HttpPost]
        public Task<CommonHttpResponse<UserViewModel>> LoginAsync(LoginContract contract)
        {
            return _userLogic.LoginAsync(contract);
        }

        [HttpPost]
        public Task<CommonHttpResponse<UserViewModel>> GetUserInfoAsync(LoginTokenContract contract)
        {
            return _userLogic.GetUserInfoAsync(contract);
        }

        [HttpPost]
        public Task<CommonHttpResponse<UserViewModel>> LoginByRefreshAsync(LoginTokenContract contract)
        {
            return _userLogic.LoginByRefreshAsync(contract);
        }

        [HttpGet]
        public Task<CommonHttpResponse<ShortUserViewModel>> GetUserByIdAsync([FromQuery]GetContract contract)
        {
            return _userLogic.GetUserByIdAsync(contract);
        }

    }
}
