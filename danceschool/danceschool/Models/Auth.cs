
using System.ComponentModel.DataAnnotations;

namespace danceschool.Models
{
    public class SignUpRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string UserName { get; set; }
    }

    public class SignUpResult
    {
        public bool Succeeded { get; set; }
    }

    public class AuthorizationResult
    {
        public string Role { get; set; }
    }

    public class Token
    {
        public string IdToken { get; set; }
    }
}
