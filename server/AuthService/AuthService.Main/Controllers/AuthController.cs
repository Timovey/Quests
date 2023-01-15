using AuthService.Core.BusinessLogic;
using AuthService.DataContracts.Common;
using AuthService.DataContracts.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Main.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]

    public class AuthController : Controller
    {
        private UserLogic _userLogic;
        public AuthController(UserLogic userLogic)
        {                         
            _userLogic = userLogic;
        }

        [HttpPost]
        public Task<CommonHttpResponse<UserViewModel>> Register(CreateUserContract createContract)
        {
            return _userLogic.Register(createContract);
        }

    }
}
