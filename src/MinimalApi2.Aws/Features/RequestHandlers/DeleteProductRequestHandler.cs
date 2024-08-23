using MediatR;
using MinimalApi2.Aws.Abstractions;
using MinimalApi2.Aws.Entities;
using MinimalApi2.Aws.Features.Requests;
using MinimalApi2.Aws.Models;
using MinimalApi2.Aws.Models.Responses;

namespace MinimalApi2.Aws.Features.RequestHandlers
{
    public class DeleteProductRequestHandler(IRepository<Product> _productRepository/*, IImageService _imageService*/) : IRequestHandler<DeleteProductRequest, ApiResponseModel<bool>>
    {
        public async Task<ApiResponseModel<bool>> Handle(DeleteProductRequest request, CancellationToken cancellationToken)
        {
            Product? product = await _productRepository.GetAsync(p => p.Id == request.ProductId, false, p => p.ImageProducts);

            if (product is not null)
            {
                //    if (!await _imageService.ExistBucket(Constants.S3Bucket.ProductImage))
                //        await _imageService.CreateBucketAsync(Constants.S3Bucket.ProductImage);

                //foreach (var image in product.ImageProducts)
                //    await _imageService.DeleteFileAsync(Constants.S3Bucket.ProductImage, image.Name);
                // Burada S3 den resimelri silmek gerekecek !
            }

            bool res = await _productRepository.DeleteAsync(request.ProductId);

            return res is true ? ApiResponseModel<bool>.CreateSuccess(res) : ApiResponseModel<bool>.CreateFailure<bool>();
        }
    }
}
