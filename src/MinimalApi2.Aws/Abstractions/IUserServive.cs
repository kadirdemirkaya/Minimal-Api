using MinimalApi2.Aws.Models.Responses;
using MinimalApi2.Aws.Models.Users;

namespace MinimalApi2.Aws.Abstractions
{
    public interface IUserServive
    {
        Task<UserResponseModel> UserRegisterAsync(RegisterViewModel registerViewModel);

        Task<UserResponseModel> UserLoginAsync(LoginViewModel loginViewModel);
    }
}
