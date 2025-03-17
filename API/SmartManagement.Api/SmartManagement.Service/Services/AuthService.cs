using AutoMapper;
using SmartManagement.Core.DTOs;
using SmartManagement.Core.Models;
using SmartManagement.Core.Repositories;
using SmartManagement.Core.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartManagement.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AuthService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public User Login(LoginRequest loginRequest)
        {
            User user = _userRepository.GetUserByEmail(loginRequest.Email);
            if (user != null && user.Password == loginRequest.Password && user.Name == loginRequest.UserName)
            {
                return user;
            }
            return null;
        }
        public User Register(RegisterUserDto userRegister)
        {
            if (userRegister == null)
                throw new ArgumentNullException(nameof(userRegister), "User cannot be null.");

            var existingUser = _userRepository.GetUserByEmail(userRegister.Email);
            if (existingUser != null)
                throw new InvalidOperationException("A user with this email already exists.");
            var user = _mapper.Map<User>(userRegister);

            _userRepository.AddUser(user);
            return user;

        }
    }
}
