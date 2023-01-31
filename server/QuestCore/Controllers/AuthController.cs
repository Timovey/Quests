using AuthService.DataContracts.Interfaces;
using AuthService.DataContracts.User;
using CommonInfrastructure.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [HttpGet]
        [Authorize]
        public async Task<CommonHttpResponse<UserViewModel>> GetUserInfoAsync()
        {
            var userName = User.FindFirst(ClaimTypes.Name);
            var password = User.FindFirst(ClaimTypes.Hash);

            if (User.Identity.IsAuthenticated && userName != null && password != null)
            {    
                return await _authApi.GetUserInfoAsync(new LoginContract()
                {
                    Password = password.Value,
                    UserName = userName.Value
                });
            }
            else
            {
                return new CommonHttpResponse<UserViewModel>
                {
                    Success = false,
                    StatusCode = System.Net.HttpStatusCode.Unauthorized,
                    Errors = new string[] { }
                };
            }
        }


    }
}
