using MediatR;
using MinimalApi2.Aws.Abstractions;
using MinimalApi2.Aws.Entities;
using MinimalApi2.Aws.Features.Requests;
using MinimalApi2.Aws.Models;
using MinimalApi2.Aws.Models.Product;
using MinimalApi2.Aws.Models.Responses;

namespace MinimalApi2.Aws.Features.RequestHandlers
{
    public class GetProductByIdRequestHandler(IRepository<Product> _productRepository, IImageService _imageService) : IRequestHandler<GetProductByIdRequest, ApiResponseModel<GetProductModel>>
    {
        public async Task<ApiResponseModel<GetProductModel>> Handle(GetProductByIdRequest request, CancellationToken cancellationToken)
        {
            Product? product = await _productRepository.GetAsync(p => p.Id == request.ProductId, false, p => p.ImageProducts);
            GetProductModel getProductModel = new();
            List<string> urls = new();

            foreach (var image in product.ImageProducts)
            {
                //Burada resimlerin linkleri alınacak ilk önce ! Sonra modele koyulacak

                urls.Add(await _imageService.GetFileByNameAsync(Constants.S3Bucket.ProductImage, image.Name));
            }

            getProductModel = getProductModel.CreateModel(product.Name, product.Description, product.Price, urls);
            urls.Clear();

            return ApiResponseModel<GetProductModel>.CreateSuccess(getProductModel);
        }
    }
}
