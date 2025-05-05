using System.ComponentModel.DataAnnotations;
using TASKHIVE.Model;

namespace TASKHIVE.DTO.Role
{
    public class RoleDto
    {
        [Key]
        [Required]
        public int roleId { get; set; }

        [Required]
        public UserRole userRole { get; set; }
    }
}
