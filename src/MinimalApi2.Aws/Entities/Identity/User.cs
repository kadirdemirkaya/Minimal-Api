using Microsoft.AspNetCore.Identity;
using MinimalApi2.Aws.Entities.Base;

namespace MinimalApi2.Aws.Entities.Identity
{
    public class User : IdentityUser<Guid>, IBaseEntity
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ChangedAt { get; set; }
        public bool IsActive { get; set; } = true;


        public User()
        {

        }

        public User(Guid id, string name, string email, string phoneNumber)
        {
            Id = id;
            UserName = name;
            Email = email;
            NormalizedEmail = email.ToUpper();
            PhoneNumber = phoneNumber;
        }

        public User(string name, string email, string phoneNumber)
        {
            Id = Guid.NewGuid();
            UserName = name;
            Email = email;
            NormalizedEmail = email.ToUpper();
            PhoneNumber = phoneNumber;
        }

        public static User Create(Guid id, string name, string email, string phoneNumber)
            => new(id, name, email, phoneNumber);

        public static User Create(string name, string email, string phoneNumber)
            => new(name, email, phoneNumber);
    }
}
