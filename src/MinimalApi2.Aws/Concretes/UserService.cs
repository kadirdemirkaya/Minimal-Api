using Microsoft.AspNetCore.Identity;
using MinimalApi2.Aws.Abstractions;
using MinimalApi2.Aws.Entities.Identity;
using MinimalApi2.Aws.Models;
using MinimalApi2.Aws.Models.Responses;
using MinimalApi2.Aws.Models.Users;
using System.Reflection.Metadata;

namespace MinimalApi2.Aws.Concretes
{
    public class UserService(UserManager<User> userManager, ITokenService tokenService, ILogger<UserService> logger) : IUserServive
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly ITokenService _tokenService = tokenService;
        private readonly ILogger<UserService> _logger = logger;
        public async Task<UserResponseModel> UserLoginAsync(LoginViewModel loginViewModel)
        {
            if (loginViewModel is null)
                throw new NullReferenceException($"{nameof(LoginViewModel)} is null !");

            User? user = await _userManager.FindByEmailAsync(loginViewModel.Email);

            if (user is null)
                return new()
                {
                    IsSuccess = false,
                    Errors = new string[] { "User Not found" }
                };

            bool result = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);

            if (!result)
                return new()
                {
                    IsSuccess = false,
                    Errors = new string[] { "Invalid user inputs" }
                };

            var token = _tokenService.GenerateToken(user);

            return new()
            {
                IsSuccess = true,
                Token = token.AccessToken
            };
        }

        public async Task<UserResponseModel> UserRegisterAsync(RegisterViewModel registerViewModel)
        {
            if (registerViewModel is null)
                throw new NullReferenceException($"{nameof(RegisterViewModel)} is null !");

            User user = User.Create(registerViewModel.Name, registerViewModel.Email, registerViewModel.PhoneNumber);

            IdentityResult result = await _userManager.CreateAsync(user, registerViewModel.Password);

            if (result.Succeeded)
            {
                IdentityResult? roleResult = await _userManager.AddToRoleAsync(user, Constants.Role.User);
                return roleResult.Succeeded is true ? new() { IsSuccess = true } : new() { IsSuccess = false };
            }
            else
            {
                string[] errorMessages = result.Errors.Select(e => e.Description).ToArray();
                return new()
                {
                    IsSuccess = false,
                    Errors = errorMessages
                };
            }
        }
    }
}
