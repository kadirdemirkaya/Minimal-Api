using MediatR;
using MinimalApi2.Aws.Models.Product;
using MinimalApi2.Aws.Models.Responses;

namespace MinimalApi2.Aws.Features.Requests
{
    public class CreateProductRequest : IRequest<ApiResponseModel<bool>>
    {
        public CreateProductViewModel CreateProductViewModel { get; set; }

        public CreateProductRequest(CreateProductViewModel createProductViewModel)
        {
            CreateProductViewModel = createProductViewModel;
        }
    }
}
