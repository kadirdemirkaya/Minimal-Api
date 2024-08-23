using MinimalApi2.Aws.Entities.Identity;
using MinimalApi2.Aws.Models;

namespace MinimalApi2.Aws.Abstractions
{
    public interface ITokenService
    {
        TokenModel GenerateToken(User user);
    }
}
