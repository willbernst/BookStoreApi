using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.API.Dto
{
    public class RegisterUserDto
    {
        [Required(ErrorMessage = "The {0} field is required")]
        [EmailAddress(ErrorMessage = "The {0} field is in invalid format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The {0} field is required")]
        [StringLength(100, ErrorMessage = "The {0} field must be between {2} and {1} characters", MinimumLength = 6)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Password don't match")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginUserDto
    {
        [Required(ErrorMessage = "The {0} field is required")]
        [EmailAddress(ErrorMessage = "The {0} field is in invalid format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The {0} field is required")]
        [StringLength(100, ErrorMessage = "The {0} field must be between {2} and {1} characters", MinimumLength = 6)]
        public string Password { get; set; }
    }

    public class UserTokenDto
    {
        public string Id { get; set; }
        
        public string Email { set; get; }

        public IEnumerable<ClaimDto> Claims { get; set; }
    }

    public class ClaimDto
    {
        public string Value { get; set; }
        
        public string Type { get; set; }
    }
}
