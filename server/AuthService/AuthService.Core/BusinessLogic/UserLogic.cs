using AuthService.Core.Helpers;
using AuthService.Database.Interfaces;
using AuthService.Database.Models;
using AuthService.DataContracts.User;
using CommonInfrastructure.Extension;
using CommonInfrastructure.Http;
using CommonInfrastructure.Http.Helpers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Net;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Diagnostics.Contracts;
using System.Xml.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Core.BusinessLogic
{
    public class UserLogic
    {
        private IUserStorage _userStorage;
        private GenerateTokenHelper _tokenHelper;
        private PasswordHashHelper _hashHelper;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        public UserLogic(IUserStorage userStorage, GenerateTokenHelper tokenHelper, PasswordHashHelper hashHelper, 
            UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {                              
            _userStorage = userStorage;
            _tokenHelper = tokenHelper;
            _hashHelper = hashHelper;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }
        //public async Task<CommonHttpResponse<UserViewModel>> RegisterAsync(CreateUserContract createContract)
        //{
        //    if (createContract == null)
        //    {
        //        return CommonHttpHelper.BuildErrorResponse<UserViewModel>(initialError:
        //            "Пустой запрос");
        //    }
        //    //если пользователь с таким логином уже существует
        //    if (!await _userStorage.IsUserUniqueAsync(createContract))
        //    {
        //        return CommonHttpHelper.BuildErrorResponse<UserViewModel>(initialError: 
        //            "Пользователь с таким логином уже существует");
        //    }
        //    try
        //    {
        //        createContract.Password = _hashHelper.ComputeHashPassword(createContract.Password);
        //        var result = await _userStorage.AddUserAsync(createContract);
        //        result = AddUserTokens(result, createContract.Password);

        //        return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
        //    }
        //    catch (Exception ex)
        //    {
        //        return CommonHttpHelper.BuildErrorResponse<UserViewModel>(
        //            HttpStatusCode.InternalServerError,
        //            ex.ToExceptionDetails(),
        //             $"Ошибка выполнения метода {nameof(RegisterAsync)}");
        //    }
        //}

        //public async Task<CommonHttpResponse<UserViewModel>> LoginAsync(LoginContract contract)
        //{
        //    if (contract == null)
        //    {
        //        return CommonHttpHelper.BuildErrorResponse<UserViewModel>(initialError:
        //            "Пустой запрос");
        //    }
        //    string hashPassword = _hashHelper.ComputeHashPassword(contract.Password);
        //    var result = await _userStorage.GetUserAsync(contract.UserName, hashPassword);
        //    if (result == null)
        //    {
        //        return CommonHttpHelper.BuildErrorResponse<UserViewModel>(initialError:
        //            "Нет пользователя с таким логином и паролем");
        //    }
        //    try
        //    {
        //        result = AddUserTokens(result, hashPassword);


        //        return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
        //    }
        //    catch (Exception ex)
        //    {
        //        return CommonHttpHelper.BuildErrorResponse<UserViewModel>(
        //            HttpStatusCode.InternalServerError,
        //            ex.ToExceptionDetails(),
        //             $"Ошибка выполнения метода {nameof(RegisterAsync)}");
        //    }
        //}

        //public async Task<CommonHttpResponse<UserViewModel>> GetUserInfoAsync(LoginContract contract)
        //{
        //    if (contract == null || contract.UserName == null || contract.Password == null)
        //    {
        //        return CommonHttpHelper.BuildErrorResponse<UserViewModel>(initialError:
        //            "Пустой запрос", statusCode: HttpStatusCode.Unauthorized);
        //    }
        //    try
        //    {
        //        var result = await _userStorage.GetUserAsync(contract.UserName, contract.Password);

        //        if(result != null)
        //        {
        //            return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
        //        }
        //        else
        //        {
        //            return CommonHttpHelper.BuildErrorResponse<UserViewModel>(initialError:
        //          "Нет такого пользователя");
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        return CommonHttpHelper.BuildErrorResponse<UserViewModel>(
        //            HttpStatusCode.InternalServerError,
        //            ex.ToExceptionDetails(),
        //             $"Ошибка выполнения метода {nameof(GetUserInfoAsync)}");
        //    }
        //}

        //private UserViewModel AddUserTokens(UserViewModel user, string password)
        //{
        //    user.Token = _tokenHelper.GenerateJwtToken(user, password);
        //    user.RefreshToken = _tokenHelper.GenerateJwtRefreshToken(user, password);

        //    return user;
        //}

        public async Task<CommonHttpResponse<UserViewModel>> RegisterAsync(CreateUserContract createContract)
        {
            if (createContract == null)
            {
                return CommonHttpHelper.BuildErrorResponse<UserViewModel>(initialError:
                    "Пустой запрос");
            }
            try
            {
                var userExists = await _userManager.FindByNameAsync(createContract.UserName);
                if (userExists != null)
                    return CommonHttpHelper.BuildErrorResponse<UserViewModel>(initialError:
                           "Пользователь с таким логином уже существует");

                string hashPassword = _hashHelper.ComputeHashPassword(createContract.Password);

                ApplicationUser user = new()
                {
                    PasswordHash = hashPassword,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = createContract.UserName,
                    Name = createContract.Name,
                    Surname = createContract.Surname,
                };
                var result = await _userManager.CreateAsync(user, hashPassword);
                
                if (!result.Succeeded)
                {
                    return CommonHttpHelper.BuildErrorResponse<UserViewModel>(initialError:
                        "Не удалось создать пользователя");
                }


                return CommonHttpHelper.BuildSuccessResponse(_mapper.Map<UserViewModel>(user), HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<UserViewModel>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                     $"Ошибка выполнения метода {nameof(RegisterAsync)}");
            }
        }

        public async Task<CommonHttpResponse<UserViewModel>> LoginAsync(LoginContract contract)
        {
            if (contract == null)
            {
                return CommonHttpHelper.BuildErrorResponse<UserViewModel>(initialError:
                    "Пустой запрос");
            }
            try
            {
                string hashPassword = _hashHelper.ComputeHashPassword(contract.Password);

                var user = await _userManager.FindByNameAsync(contract.UserName);

                if (user != null && await _userManager.CheckPasswordAsync(user, contract.Password))
                {
                    var userRoles = await _userManager.GetRolesAsync(user);

                    var token = _tokenHelper.GenerateJwtToken(user, hashPassword, userRoles);
                    var refreshToken = _tokenHelper.GenerateJwtRefreshToken();

                    user.RefreshToken = refreshToken.Item1;
                    user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(refreshToken.Item2);

                    await _userManager.UpdateAsync(user);

                    return CommonHttpHelper.BuildSuccessResponse(_mapper.Map<UserViewModel>(user), HttpStatusCode.OK);
                }
                return CommonHttpHelper.BuildErrorResponse<UserViewModel>(initialError:
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

        public async Task<CommonHttpResponse<UserViewModel>> GetUserInfoAsync(LoginContract contract)
        {
            if (contract == null || contract.UserName == null || contract.Password == null)
            {
                return CommonHttpHelper.BuildErrorResponse<UserViewModel>(initialError:
                    "Пустой запрос", statusCode: HttpStatusCode.Unauthorized);
            }
            try
            {
                var result = await _userStorage.GetUserAsync(contract.UserName, contract.Password);

                if (result != null)
                {
                    return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
                }
                else
                {
                    return CommonHttpHelper.BuildErrorResponse<UserViewModel>(initialError:
                  "Нет такого пользователя");
                }

            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<UserViewModel>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                     $"Ошибка выполнения метода {nameof(GetUserInfoAsync)}");
            }
        }

        private UserViewModel AddUserTokens(UserViewModel user, string password)
        {
            user.Token = _tokenHelper.GenerateJwtToken(user, password);
            user.RefreshToken = _tokenHelper.GenerateJwtRefreshToken(user, password);

            return user;
        }

    }
}
