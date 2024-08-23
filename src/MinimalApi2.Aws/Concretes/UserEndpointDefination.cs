using MediatR;
using Microsoft.AspNetCore.Mvc;
using MinimalApi2.Aws.Abstractions;
using MinimalApi2.Aws.Features.Requests;
using MinimalApi2.Aws.Filter;
using MinimalApi2.Aws.Models.Responses;
using MinimalApi2.Aws.Models.Users;

namespace MinimalApi2.Aws.Concretes
{
    public class UserEndpointDefination : IEndpointDefination
    {
        public void RegisterEndpoints(WebApplication app)
        {
            var posts = app.MapGroup("/api/user");

            posts.MapPost("/login", LoginAsync)
                 .AddEndpointFilter<ModelValidationFilter>()
                 .WithName("login");

            posts.MapPost("/register", RegisterAsync)
                 .AddEndpointFilter<ModelValidationFilter>()
                 .WithName("register");
        }

        private async Task<ActionResult<ApiResponseModel<UserResponseModel>>> LoginAsync(IMediator _mediator, LoginViewModel loginCommandRequest)
        {
            LoginRequest loginRequest = new(loginCommandRequest);
            ApiResponseModel<UserResponseModel> userResponseModel = await _mediator.Send(loginRequest);
            return userResponseModel;
        }

        private async Task<ActionResult<ApiResponseModel<UserResponseModel>>> RegisterAsync(IMediator _mediator, RegisterViewModel registerViewModel)
        {
            RegisterRequest registerRequest = new(registerViewModel);
            ApiResponseModel<UserResponseModel> userResponseModel = await _mediator.Send(registerRequest);
            return userResponseModel;
        }
    }
}
