using System.ComponentModel.DataAnnotations;

namespace TASKHIVE.DTO.Role
{
    public class CreateLabelDto
    {
        [Key]
        [Required]
        public int roleId { get; set; }
        public string roleName { get; set; }
    }
}
