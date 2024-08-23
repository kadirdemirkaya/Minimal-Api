using Amazon.S3;
using Amazon.S3.Model;
using Amazon.Util.Internal;
using MinimalApi2.Aws.Abstractions;
using MinimalApi2.Aws.Models;

namespace MinimalApi2.Aws.Concretes
{
    public class ImageService : IImageService
    {
        private readonly IAmazonS3 _amazonS3;

        public ImageService(IAmazonS3 amazonS3)
        {
            _amazonS3 = amazonS3;
        }

        public async Task<bool> CreateBucketAsync(string bucketName)
        {
            bool bucketExists = await _amazonS3.DoesS3BucketExistAsync(bucketName);
            if (bucketExists)
                return false;
            await _amazonS3.PutBucketAsync(bucketName);
            return true;
        }

        public async Task<bool> DeleteBucketAsync(string bucketName)
        {
            await _amazonS3.DeleteBucketAsync(bucketName);
            return true;
        }

        public async Task<bool> ExistBucket(string bucketName)
        {
            bool res = await _amazonS3.DoesS3BucketExistAsync(bucketName);
            return res;
        }

        public async Task<ListBucketsResponse> GetAllBucketAsync()
        {
            ListBucketsResponse bucketDatas = await _amazonS3.ListBucketsAsync();
            return bucketDatas;
        }

        public async Task<bool> DeleteFileAsync(string bucketName, string fileName)
        {
            bool bucketExists = await _amazonS3.DoesS3BucketExistAsync(bucketName);
            if (!bucketExists)
                return false;
            await _amazonS3.DeleteObjectAsync(bucketName, fileName);
            return true;
        }

        public async Task<bool> UploadFileAsync(IFormFile file, string bucketName, string fileName, string? prefix)
        {
            try
            {

                bool bucketExists = await _amazonS3.DoesS3BucketExistAsync(bucketName);
                if (!bucketExists)
                    return false;
                PutObjectRequest request = new()
                {
                    BucketName = bucketName,
                    Key = String.IsNullOrEmpty(prefix) ? $"{fileName}" : $"{prefix?.TrimEnd('/')}/{fileName}",
                    InputStream = file.OpenReadStream(),
                    ContentType = file.ContentType,
                };
                //request.Metadata.Add("Content-Type", file.ContentType);
                await _amazonS3.PutObjectAsync(request);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> GetFileByNameAsync(string bucketName, string fileName)
        {
            bool bucketExists = await _amazonS3.DoesS3BucketExistAsync(bucketName);
            if (!bucketExists)
                return null;
            GetPreSignedUrlRequest request = new GetPreSignedUrlRequest
            {
                BucketName = bucketName,
                Key = fileName,
                Expires = DateTime.Now.AddHours(30)
            };
            return _amazonS3.GetPreSignedURL(request);
        }

        public async Task<List<S3ObjectModel>> GetAllFilesAsync(string bucketName, string? prefix)
        {
            bool bucketExists = await _amazonS3.DoesS3BucketExistAsync(bucketName);
            if (!bucketExists)
                return null;
            ListObjectsV2Request request = new()
            {
                BucketName = bucketName,
                Prefix = prefix
            };
            ListObjectsV2Response response = await _amazonS3.ListObjectsV2Async(request);
            List<S3ObjectModel> objectDatas = response.S3Objects.Select(@object =>
            {
                GetPreSignedUrlRequest urlRequest = new()
                {
                    BucketName = bucketName,
                    Key = @object.Key,
                    Expires = DateTime.UtcNow.AddMinutes(30)
                };
                return new S3ObjectModel
                {
                    Name = @object.Key,
                    Url = _amazonS3.GetPreSignedURL(urlRequest)
                };
            }).ToList();
            return objectDatas;
        }
    }
}
