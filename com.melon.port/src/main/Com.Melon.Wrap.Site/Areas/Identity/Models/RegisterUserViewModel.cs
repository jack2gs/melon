using System.ComponentModel.DataAnnotations;

namespace Com.Melon.Wrap.Site.Areas.Users.Models
{
    public class RegisterUserViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "The email should be less than 20 characters")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(20, MinimumLength=6, ErrorMessage = "The passowrd should be between 6 and 20 characters")]
        public string Password { get; set; }
    }
}
