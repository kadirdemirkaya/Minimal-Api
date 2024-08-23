using MediatR;
using MinimalApi2.Aws.Models.Product;
using MinimalApi2.Aws.Models.Responses;

namespace MinimalApi2.Aws.Features.Requests
{
    public class GetAllProductsRequest : IRequest<ApiResponseModel<List<GetAllProductsModel>>>
    {
    }
}
