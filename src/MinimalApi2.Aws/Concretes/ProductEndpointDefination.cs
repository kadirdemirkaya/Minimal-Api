using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MinimalApi2.Aws.Abstractions;
using MinimalApi2.Aws.Features.Requests;
using MinimalApi2.Aws.Filter;
using MinimalApi2.Aws.Models.Product;
using MinimalApi2.Aws.Models.Responses;

namespace MinimalApi2.Aws.Concretes
{
    public class ProductEndpointDefination : IEndpointDefination
    {
        public void RegisterEndpoints(WebApplication app)
        {
            var posts = app.MapGroup("/api/product");

            posts.MapGet("/test", Test)
                 .AddEndpointFilter<ModelValidationFilter>()
                 .WithName("test");

            posts.MapPost("/create-product", CreateProductAsync)
                 .AddEndpointFilter<ModelValidationFilter>()
                 .WithName("create-product")
                 .WithMetadata(new ConsumesAttribute("multipart/form-data"));

            posts.MapGet("/get-all-products", GetAllProductsAsync)
                 .AddEndpointFilter<ModelValidationFilter>()
                 .WithName("get-all-products");

            posts.MapGet("get-product-by-id/{id:guid}", GetByIdAsync)
                  .AddEndpointFilter<ModelValidationFilter>()
                  .WithName("get-product-by-id");

            posts.MapDelete("delete-product/{id:guid}", DeleteProductAsync)
                 .AddEndpointFilter<ModelValidationFilter>()
                 .WithName("delete-product");
        }

        private async Task<IResult> Test()
        {
            return TypedResults.Ok();
        }

        private async Task<IResult> CreateProductAsync(IMediator _mediator, [FromForm] CreateProductViewModel createProductViewModel)
        {
            CreateProductRequest createProductRequest = new(createProductViewModel);

            ApiResponseModel<bool> response = await _mediator.Send(createProductRequest);
            if (response.Success)
                return TypedResults.Ok(response); // BURAYA GETBYID'NIN MAP ISTEGINE YONLENDIRILEBILINIR !
            return TypedResults.BadRequest();
        }


        //private async Task<IResult> CreateProductAsync(IMediator _mediator, [FromForm] CreateProductViewModel createProductViewModel)
        //{
        //    CreateProductRequest createProductRequest = new(createProductViewModel);
        //    ApiResponseModel<bool> response = await _mediator.Send(createProductRequest);
        //    if (response.Success)
        //        return TypedResults.Ok(response); // BURAYA GETBYID'NIN MAP ISTEGINE YONLENDIRILEBILINIR !
        //    return TypedResults.BadRequest();
        //}

        public async Task<ActionResult<ApiResponseModel<List<GetAllProductsModel>>>> GetAllProductsAsync(IMediator _mediator)
        {
            GetAllProductsRequest getAllProductsRequest = new();
            ApiResponseModel<List<GetAllProductsModel>>? getAllProductsModels = await _mediator.Send(getAllProductsRequest);
            return getAllProductsModels;
        }


        public async Task<ActionResult<ApiResponseModel<GetProductModel>>> GetByIdAsync(IMediator _mediator, Guid id)
        {
            GetProductByIdRequest getProductByIdRequest = new(id);
            ApiResponseModel<GetProductModel>? getProductModel = await _mediator.Send(getProductByIdRequest);
            return getProductModel;
        }

        public async Task<IResult> DeleteProductAsync(IMediator _mediator, Guid id)
        {
            DeleteProductRequest deleteProductRequest = new(id);
            ApiResponseModel<bool> response = await _mediator.Send(deleteProductRequest);
            return response.Success is true ? TypedResults.Ok(response) : TypedResults.BadRequest();
        }

    }
}
