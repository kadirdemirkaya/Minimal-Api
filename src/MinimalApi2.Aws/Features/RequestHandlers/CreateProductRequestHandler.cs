using MediatR;
using MinimalApi2.Aws.Abstractions;
using MinimalApi2.Aws.Entities;
using MinimalApi2.Aws.Features.Requests;
using MinimalApi2.Aws.Models;
using MinimalApi2.Aws.Models.Responses;

namespace MinimalApi2.Aws.Features.RequestHandlers
{
    public class CreateProductRequestHandler(IRepository<Product> _productRepository, IImageService _imageService) : IRequestHandler<CreateProductRequest, ApiResponseModel<bool>>
    {
        public async Task<ApiResponseModel<bool>> Handle(CreateProductRequest request, CancellationToken cancellationToken)
        {
            Product product = Product.Create(request.CreateProductViewModel.Name, request.CreateProductViewModel.Description, Convert.ToDouble(request.CreateProductViewModel.Price));

            //if (!await _imageService.ExistBucket(Constants.S3Bucket.ProductImage))
            //{
            //    await _imageService.CreateBucketAsync(Constants.S3Bucket.ProductImage);
            //}

            #region OLD
            //foreach (var image in request.CreateProductViewModel.Images)
            //{
            //    // To Do : 
            //    //Burada resim isim olarak ayarlanacak ve image serviste eklenecek sonra image olarak tabloya eklenecek !

            //    bool res = await _imageService.UploadFileAsync(image, Constants.S3Bucket.ProductImage, image.FileName, null);

            //    if (res)
            //        product.AddImage(Image.Create(image.FileName, product.Id));
            //}
            #endregion

            // To Do : 
            //Burada resim isim olarak ayarlanacak ve image serviste eklenecek sonra image olarak tabloya eklenecek !
            bool res = await _imageService.UploadFileAsync(request.CreateProductViewModel.Images, Constants.S3Bucket.ProductImage, request.CreateProductViewModel.Images.FileName, null);

            if (res)
                product.AddImage(Image.Create(request.CreateProductViewModel.Images.FileName, product.Id));

            if (await _productRepository.AddAsync(product))
            {
                return await _productRepository.SaveChangesAsync() is true ? ApiResponseModel<bool>.CreateSuccess(true) : ApiResponseModel<bool>.CreateFailure<bool>();
            }

            return ApiResponseModel<bool>.CreateFailure<bool>();
        }
    }
}
