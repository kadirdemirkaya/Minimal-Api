using MediatR;
using MinimalApi2.Aws.Abstractions;
using MinimalApi2.Aws.Features.Requests;
using MinimalApi2.Aws.Models.Responses;

namespace MinimalApi2.Aws.Features.RequestHandlers
{
    public class RegisterRequestHandler(IUserServive _userServive) : IRequestHandler<RegisterRequest, ApiResponseModel<UserResponseModel>>
    {
        public async Task<ApiResponseModel<UserResponseModel>> Handle(RegisterRequest request, CancellationToken cancellationToken)
        {
            UserResponseModel userResponseModel = await _userServive.UserRegisterAsync(new() { Email = request.RegisterViewModel.Email, Name = request.RegisterViewModel.Name, Password = request.RegisterViewModel.Password, PhoneNumber = request.RegisterViewModel.PhoneNumber });

            if (userResponseModel.IsSuccess)
                return ApiResponseModel<UserResponseModel>.CreateSuccess(userResponseModel);
            return ApiResponseModel<UserResponseModel>.CreateFailure<UserResponseModel>(userResponseModel.Errors);
        }
    }
}
