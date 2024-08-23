using Amazon.S3.Model;
using MinimalApi2.Aws.Models;

namespace MinimalApi2.Aws.Abstractions
{
    public interface IImageService
    {
        Task<bool> ExistBucket(string bucketName);
        Task<bool> CreateBucketAsync(string bucketName);
        Task<bool> DeleteBucketAsync(string bucketName);
        Task<bool> DeleteFileAsync(string bucketName, string fileName);
        Task<ListBucketsResponse> GetAllBucketAsync();
        Task<List<S3ObjectModel>> GetAllFilesAsync(string bucketName, string? prefix);
        Task<string> GetFileByNameAsync(string bucketName, string fileName);
        Task<bool> UploadFileAsync(IFormFile file, string bucketName, string fileName, string? prefix);
    }
}
