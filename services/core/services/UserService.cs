using System.Collections.Generic;
using System.Threading.Tasks;
using Microservices.Services.Core.Entities;
using Microservices.Services.Core.Interface.Services;
using Microservices.Services.Core.Models;
using Microservices.Services.Core.Repositories;

namespace Microservices.Services.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthRepository _authRepository;

        public UserService(IUserRepository userRepository, IAuthRepository authRepository)
        {
            _userRepository = userRepository;
            _authRepository = authRepository;
        }

        public async Task<User> AddNewUser(User user)
        {
            return await _userRepository.AddNewUser(user);
        }

        public async Task<string> Authenticate(LoginModel model)
        {
            return await _authRepository.Authenticate(model.Username, model.Password);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _userRepository.GetAll();
        }
    }
}