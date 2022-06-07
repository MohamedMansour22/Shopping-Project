using SQL_Provider.Enums;
using System.ComponentModel.DataAnnotations;

namespace Business.Entites.Parameters
{
    public class SignupParameters
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        public DateTime Birthdate { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public Role Role { get; set; }
    }
}
