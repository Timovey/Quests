using AuthService.DataContracts.User;

namespace AuthService.Database.Interfaces
{
    public interface IUserStorage
    {
        public Task<UserViewModel> AddUserAsync(CreateUserContract createContract);

        public Task<bool> IsUserUniqueAsync(CreateUserContract createContract);

        public Task<UserViewModel> GetUserAsync(string userName, string password);
    }
}
