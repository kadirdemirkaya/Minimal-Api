using System.ComponentModel.DataAnnotations;

namespace MinimalApi2.Aws.Models.Users
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }


        [Required]
        [MinLength(3)]
        public string Password { get; set; }
    }
}
