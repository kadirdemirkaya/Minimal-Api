using MediatR;
using MinimalApi2.Aws.Models.Responses;

namespace MinimalApi2.Aws.Features.Requests
{
    public class DeleteProductRequest : IRequest<ApiResponseModel<bool>>
    {
        public Guid ProductId { get; set; }

        public DeleteProductRequest(Guid productId)
        {
            ProductId = productId;
        }
    }
}
