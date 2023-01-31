using AuthService.Core.BusinessLogic;
using AuthService.DataContracts.Interfaces;
using AuthService.DataContracts.User;
using CommonInfrastructure.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthService.Main.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]

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
            return _userLogic.Register2Async(createContract);
        }

        [HttpPost]
        public Task<CommonHttpResponse<UserViewModel>> LoginAsync(LoginContract contract)
        {
            return _userLogic.Login2Async(contract);
        }

        [HttpPost]
        public Task<CommonHttpResponse<UserViewModel>> GetUserInfoAsync(LoginContract contract)
        {
            return _userLogic.GetUserInfoAsync(contract);
        }

        //[HttpPost]
        //public Task<CommonHttpResponse<string>> GetTokenByRefresh(string token)
        //{
        //    return _userLogic.GetUserInfoAsync(contract);
        //}

    }
}
