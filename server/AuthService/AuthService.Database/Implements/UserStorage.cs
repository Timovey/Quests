using AuthService.Database.Interfaces;
using AuthService.Database.Models;
using AuthService.DataContracts.User;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace AuthService.Database.Implements
{
    public class UserStorage : IUserStorage
    {
        private readonly AuthDbContext _context;
        private readonly IMapper _mapper;
        public UserStorage(AuthDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserViewModel> AddUserAsync(CreateUserContract createContract)
        {
            var user = _mapper.Map<User>(createContract);
                
           // await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserViewModel>(user);
        }

        public async Task<bool> IsUserUniqueAsync(CreateUserContract createContract)
        {
            //var res = await _context.Users
            //    .Where(el => el.Username == createContract.UserName)
            //    .AsNoTracking()
            //    .ToListAsync();

            // return res.Count == 0;
            return true;
        }

        public async Task<UserViewModel> GetUserAsync(string userName, string password)
        {
            //var res = await _context.Users
            //    .Where(el => el.Username == userName)
            //    .Where(el => el.Password == password)
            //    .AsNoTracking()
            //    .FirstOrDefaultAsync();

            //return _mapper.Map<UserViewModel>(res);
            return null;
        }
    }
}
