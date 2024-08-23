using MediatR;
using MinimalApi2.Aws.Models.Responses;
using MinimalApi2.Aws.Models.Users;

namespace MinimalApi2.Aws.Features.Requests
{
    public class LoginRequest : IRequest<ApiResponseModel<UserResponseModel>>
    {
        public LoginViewModel LoginViewModel { get; set; }

        public LoginRequest(LoginViewModel loginViewModel)
        {
            LoginViewModel = loginViewModel;
        }
    }
}
