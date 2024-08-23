using Microsoft.AspNetCore.Identity;
using MinimalApi2.Aws.Entities.Base;

namespace MinimalApi2.Aws.Entities.Identity
{
    public class Role : IdentityRole<Guid>, IBaseEntity
    {
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime? ChangedAt { get; private set; }
        public bool IsActive { get; private set; } = true;

    }
}
