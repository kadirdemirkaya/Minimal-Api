using MediatR;
using MinimalApi2.Aws.Abstractions;
using MinimalApi2.Aws.Concretes;
using MinimalApi2.Aws.Features.Requests;
using MinimalApi2.Aws.Models.Responses;

namespace MinimalApi2.Aws.Features.RequestHandlers
{
    public class LoginRequestHandler(IUserServive _userServive) : IRequestHandler<LoginRequest, ApiResponseModel<UserResponseModel>>
    {
        public async Task<ApiResponseModel<UserResponseModel>> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            UserResponseModel? userResponseModel = await _userServive.UserLoginAsync(new() { Email = request.LoginViewModel.Email, Password = request.LoginViewModel.Password });

            if (userResponseModel.IsSuccess)
                return ApiResponseModel<UserResponseModel>.CreateSuccess(userResponseModel);
            return ApiResponseModel<UserResponseModel>.CreateFailure<UserResponseModel>(userResponseModel.Errors);
        }
    }
}
