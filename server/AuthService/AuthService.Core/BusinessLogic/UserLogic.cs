using AuthService.Core.Helpers;
using AuthService.Database.Models;
using AuthService.DataContracts.User;
using CommonInfrastructure.Extension;
using CommonInfrastructure.Http;
using CommonInfrastructure.Http.Helpers;
using System.Net;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AuthService.Database.Implements;
using AuthService.DataContracts.RefreshToken;
using AuthService.Database.Interfaces;
using System.Diagnostics.Contracts;
using AuthService.DataContracts.CommonContracts;
using System.Collections.Generic;

namespace AuthService.Core.BusinessLogic
{
    public class UserLogic
    {
        private readonly GenerateTokenHelper _tokenHelper;
        private readonly PasswordHashHelper _hashHelper;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationUserRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IRefreshStorage _refreshStorage;
        public UserLogic(GenerateTokenHelper tokenHelper, PasswordHashHelper hashHelper, 
            UserManager<ApplicationUser> userManager, RoleManager<ApplicationUserRole> roleManager,
            IMapper mapper, IRefreshStorage refreshStorage)
        {                              
            _tokenHelper = tokenHelper;
            _hashHelper = hashHelper;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _refreshStorage = refreshStorage;   
        }

        /// <summary>
        /// Метод регистрации пользователя
        /// </summary>
        /// <param name="createContract"></param>
        /// <returns></returns>
        public async Task<CommonHttpResponse<UserViewModel>> RegisterAsync(CreateUserContract createContract)
        {
            if (createContract == null)
            {
                return CommonHttpHelper.BuildErrorResponse<UserViewModel>(initialError:
                    "Пустой запрос");
            }
            try
            {
                //находим пользователя по логину
                var userExists = await _userManager.FindByNameAsync(createContract.UserName);
                if (userExists != null)
                    return CommonHttpHelper.BuildErrorResponse<UserViewModel>(initialError:
                           "Пользователь с таким логином уже существует");

                //хешируем пароль - такой в базе и будем хранить
                string hashPassword = _hashHelper.ComputeHashPassword(createContract.Password);

                //создаем пользователя по контракту
                ApplicationUser user = new()
                {
                    PasswordHash = hashPassword,
                    UserName = createContract.UserName,
                    Name = createContract.Name,
                    Surname = createContract.Surname,
                };
                //создаем пользователя с его паролем
                var createResult = await _userManager.CreateAsync(user, hashPassword);
                
                if (!createResult.Succeeded)
                {
                    return CommonHttpHelper.BuildErrorResponse<UserViewModel>(initialError:
                        "Не удалось создать пользователя");
                }
                //получаем роли для того, чтобы интегрировать их в токен
                var userRoles = await _userManager.GetRolesAsync(user);
                //мапим во viewModel
                var result = _mapper.Map<UserViewModel>(user);
                //добавляем в к вью модели токен
                result.Token = _tokenHelper.GenerateJwtToken(user, hashPassword, userRoles);

                //генерируем refresh токен 
                string refreshToken = await AddRefreshToken(user);
                if(refreshToken.IsNullOrEmpty())
                {
                    return CommonHttpHelper.BuildErrorResponse<UserViewModel>(initialError:
                      "Не удалось создать токен обновления");
                }
                result.RefreshToken = refreshToken;

                return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<UserViewModel>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                     $"Ошибка выполнения метода {nameof(RegisterAsync)}");
            }
        }

        /// <summary>
        /// Метод логина
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        public async Task<CommonHttpResponse<UserViewModel>> LoginAsync(LoginContract contract)
        {
            if (contract == null)
            {
                return CommonHttpHelper.BuildErrorResponse<UserViewModel>(initialError:
                    "Пустой запрос");
            }
            try
            {
                //хешируем пароль - проверять будем хеши
                string hashPassword = _hashHelper.ComputeHashPassword(contract.Password);

                //находим пользователя по имени
                var user = await _userManager.FindByNameAsync(contract.UserName);

                //если такой пользователь есть и хеши паролей идентичны
                if (user != null && await _userManager.CheckPasswordAsync(user, hashPassword))
                {

                    //получаем роли для пользователя
                    var userRoles = await _userManager.GetRolesAsync(user);

                    //мапим результат
                    var result = _mapper.Map<UserViewModel>(user);
                    //добавляем токен
                    result.Token = _tokenHelper.GenerateJwtToken(user, hashPassword, userRoles);

                    //генерируем refresh токен 
                    string refreshToken = await AddRefreshToken(user);
                    if (refreshToken.IsNullOrEmpty())
                    {
                        return CommonHttpHelper.BuildErrorResponse<UserViewModel>(initialError:
                          "Не удалось создать токен обновления");
                    }
                    result.RefreshToken = refreshToken;

                    return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
                }
                return CommonHttpHelper.BuildNotFoundErrorResponse<UserViewModel>(initialError:
                    "Нет пользователя с таким логином и паролем");
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<UserViewModel>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                     $"Ошибка выполнения метода {nameof(LoginAsync)}");
            }
        }

        public async Task<CommonHttpResponse<UserViewModel>> GetUserInfoAsync(LoginTokenContract contract)
        {
            if (contract == null)
            {
                return CommonHttpHelper.BuildErrorResponse<UserViewModel>(initialError:
                    "Пустой запрос");
            }
            try
            {
                var userId = GetPrincipalFromExpiredToken(contract.Token, validateLifetime: true);
                if (userId == null)
                {
                    return CommonHttpHelper.BuildErrorResponse<UserViewModel>(initialError:
                        "Невалидный токен доступа", statusCode: HttpStatusCode.Unauthorized);
                }

                if (await CheckRefreshToken(contract.RefreshToken, userId.Value))
                {
                    //находим пользователя по Id
                    var user = await _userManager.FindByIdAsync(userId.Value.ToString());

                    if(user == null)
                    {
                        return CommonHttpHelper.BuildNotFoundErrorResponse<UserViewModel>(initialError:
                    "Нет такого пользователя");
                    }
                    //мапим во viewModel
                    var result = _mapper.Map<UserViewModel>(user);
                    //добавляем в к вью модели токен
                    result.Token = contract.Token;
                    //и прибавляем старый токен доступа
                    result.RefreshToken = contract.RefreshToken;

                    if (user != null)
                    {
                        return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
                    }
                }
                return CommonHttpHelper.BuildErrorResponse<UserViewModel>(initialError:
                    "Невалидный токен обновления", statusCode: HttpStatusCode.Unauthorized);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<UserViewModel>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                     $"Ошибка выполнения метода {nameof(GetUserInfoAsync)}");
            }
        }

        /// <summary>
        /// Метод логина по рефрешу
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        public async Task<CommonHttpResponse<UserViewModel>> LoginByRefreshAsync(LoginTokenContract contract)
        {
            if (contract == null)
            {
                return CommonHttpHelper.BuildErrorResponse<UserViewModel>(initialError:
                    "Пустой запрос");
            }
            try
            {
                //получаем null в случае неудачи (неправильно распарсили, нет инфы и т.д.)
                var userId = GetPrincipalFromExpiredToken(contract.Token);
                if(userId == null)
                {
                    return CommonHttpHelper.BuildErrorResponse<UserViewModel>(initialError:
                        "Невалидный токен доступа", statusCode: HttpStatusCode.Unauthorized);
                }

                //если валидный refresh токен
                if (await CheckRefreshToken(contract.RefreshToken, userId.Value))
                {
                    //находим пользователя по Id
                    var user = await _userManager.FindByIdAsync(userId.Value.ToString());

     
                    //получаем роли пользователя
                    var userRoles = await _userManager.GetRolesAsync(user);
                    //мапим во viewModel
                    var result = _mapper.Map<UserViewModel>(user);
                    //обновляем токен доступа
                    result.Token = _tokenHelper.GenerateJwtToken(user, user.PasswordHash, userRoles);
                    //и прибавляем старый токен обновления
                    result.RefreshToken = contract.RefreshToken;

                    if (user != null)
                    {
                        return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
                    }
                }
                return CommonHttpHelper.BuildErrorResponse<UserViewModel>(initialError:
                    "Невалидный токен обновления", statusCode: HttpStatusCode.Unauthorized);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<UserViewModel>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                     $"Ошибка выполнения метода {nameof(LoginByRefreshAsync)}");
            }
        }

        public async Task<CommonHttpResponse<ShortUserViewModel>> GetUserByIdAsync(int id)
        {
            if (id < 0)
            {
                return CommonHttpHelper.BuildErrorResponse<ShortUserViewModel>(initialError:
                    "Пустой запрос");
            }
            try
            {
                //находим пользователя по Id
                var user = await _userManager.FindByIdAsync(id.ToString());

                //мапим в простую viewModel
                var result = _mapper.Map<ShortUserViewModel>(user);

                if (user != null)
                {
                    return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
                }

                return CommonHttpHelper.BuildNotFoundErrorResponse<ShortUserViewModel>(initialError:
                    "Нет пользователя с таким id");
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<ShortUserViewModel>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                     $"Ошибка выполнения метода {nameof(GetUserByIdAsync)}");
            }
        }

        public async Task<CommonHttpResponse<IList<ShortUserViewModel>>> GetFilteredUsersAsync(GetFilteredUsersContract contract)
        {
            if (contract == null)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<ShortUserViewModel>> (initialError:
                    "Пустой запрос");
            }
            try
            {
                //находим пользователей по Id
                var users = _userManager.Users.Where(u => contract.Ids.Contains(u.Id)).ToList();

                var result = new List<ShortUserViewModel>();
                foreach (var user in users)
                {
                    //мапим в простую viewModel
                    result.Add(_mapper.Map<ShortUserViewModel>(user));
                }
                return CommonHttpHelper.BuildSuccessResponse<IList<ShortUserViewModel>>(result);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<ShortUserViewModel>>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                     $"Ошибка выполнения метода {nameof(GetFilteredUsersAsync)}");
            }
        }

        private int? GetPrincipalFromExpiredToken(string? token, bool validateLifetime = false)
        {
            //делаем валидацию токена только по ключу
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _tokenHelper.GetTokenSecurityKey(),
                ValidateLifetime = validateLifetime
            };

            //валидируем токен и получаем объект ClaimsPrincipal в котором содержатся все Claims
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                return null;

            //если в Claim содержится тип идентификатора пользователя
            if(principal.HasClaim(x => x.Type == ClaimTypes.NameIdentifier))
            {
                //получаем значение в string
                string userIdClaim = principal.FindFirstValue(ClaimTypes.NameIdentifier.ToString());

                if (userIdClaim != null)
                {
                    //пытаемся преобразовать строку идентификатора к int
                    int res = -1;
                    int.TryParse(userIdClaim, out res);
                    if (res < 0)
                    {
                        return null;
                    }
                    return res;
                }
            }
            return null;
        }

        private async Task<bool> CheckRefreshToken(string refreshToken, int userId)
        {
            var refreshTokens = await _refreshStorage.GetUserRefreshTokens(new GetRefreshesByUserContract()
            {
                UserId = userId
            });

            //и сразу находим идентичный рефрешу токен
            var resRefresh = refreshTokens.Where(el =>
                el.RefreshTokenValue.Contains(refreshToken)).FirstOrDefault();

            return resRefresh == null ? false : true;
        }

        private async Task<string> AddRefreshToken(ApplicationUser user)
        {
            var refresh = _tokenHelper.GenerateJwtRefreshToken();

            bool res = await _refreshStorage.AddRefreshToken(new CreateRefreshTokenContract()
            {
                RefreshTokenExpiryTime = DateTime.Now.AddMinutes(refresh.Item2).ToUniversalTime(),
                UserId = user.Id,
                RefreshTokenValue = refresh.Item1
            });
            if (res)
            {
                return refresh.Item1;
            }
            return null;
        }
    }
}
