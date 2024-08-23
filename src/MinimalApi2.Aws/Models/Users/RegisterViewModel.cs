using System.ComponentModel.DataAnnotations;

namespace MinimalApi2.Aws.Models.Users
{
    public class RegisterViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(10)]
        public string PhoneNumber { get; set; }

        [Required]
        [MinLength(3)]
        public string Password { get; set; }
    }
}
