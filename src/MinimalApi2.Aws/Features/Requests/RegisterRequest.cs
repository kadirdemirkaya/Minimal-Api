using MediatR;
using MinimalApi2.Aws.Models.Responses;
using MinimalApi2.Aws.Models.Users;

namespace MinimalApi2.Aws.Features.Requests
{
    public class RegisterRequest : IRequest<ApiResponseModel<UserResponseModel>>
    {
        public RegisterViewModel RegisterViewModel { get; set; }

        public RegisterRequest(RegisterViewModel registerViewModel)
        {
            RegisterViewModel = registerViewModel;
        }
    }
}
