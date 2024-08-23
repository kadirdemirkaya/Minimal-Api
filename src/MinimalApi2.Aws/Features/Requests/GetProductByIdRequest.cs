using MediatR;
using MinimalApi2.Aws.Models.Product;
using MinimalApi2.Aws.Models.Responses;

namespace MinimalApi2.Aws.Features.Requests
{
    public class GetProductByIdRequest : IRequest<ApiResponseModel<GetProductModel>>
    {
        public Guid ProductId { get; set; }

        public GetProductByIdRequest(Guid productId)
        {
            ProductId = productId;
        }
    }
}
