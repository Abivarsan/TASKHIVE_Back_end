using System.ComponentModel.DataAnnotations;
using TASKHIVE.Model;

namespace TASKHIVE.DTO.User
{
    public class UserDto
    {
        [Key]
        [Required]
        public int userId { get; set; }

        [Required]
        public string userName { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        public string password { get; set; }

        [Required]
        public string RoleName { get; set; }

    }
}
