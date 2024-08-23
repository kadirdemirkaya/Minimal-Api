using MediatR;
using MinimalApi2.Aws.Abstractions;
using MinimalApi2.Aws.Entities;
using MinimalApi2.Aws.Features.Requests;
using MinimalApi2.Aws.Models;
using MinimalApi2.Aws.Models.Product;
using MinimalApi2.Aws.Models.Responses;

namespace MinimalApi2.Aws.Features.RequestHandlers
{
    public class GetAllProductsRequestHandler(IRepository<Product> _productRepository, IImageService _imageService) : IRequestHandler<GetAllProductsRequest, ApiResponseModel<List<GetAllProductsModel>>>
    {
        public async Task<ApiResponseModel<List<GetAllProductsModel>>> Handle(GetAllProductsRequest request, CancellationToken cancellationToken)
        {
            List<Product> products = await _productRepository.GetAllAsync(null, false, p => p.ImageProducts);
            List<GetAllProductsModel> getAllProductsModels = new();
            List<string> urls = new();
            GetAllProductsModel getAllProductsModel = new();

            foreach (var product in products)
            {
                // Burada resim url'lerini listeye aktararak liste modele tanımlayacağız    

                foreach (var image in product.ImageProducts)
                {
                    urls.Add(await _imageService.GetFileByNameAsync(Constants.S3Bucket.ProductImage, image.Name));
                }

                getAllProductsModels.Add(getAllProductsModel.ModelMapper(product.Name, product.Description, product.Price, urls));
                urls.Clear();
            }

            return ApiResponseModel<GetAllProductsModel>.CreateSuccess(getAllProductsModels);
        }
    }
}
