using AuthService.Database.Models;
using AuthService.DataContracts.User;
using AutoMapper;

namespace AuthService.Database.Mappers
{
    /// <summary>
    /// Класс конфигурации маппера
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //----------------------------- User
            CreateMap<CreateUserContract, User>();
            CreateMap<User, UserViewModel>();
        }
    }

}
