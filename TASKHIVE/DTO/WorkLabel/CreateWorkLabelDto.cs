using System.ComponentModel.DataAnnotations;

namespace TASKHIVE.DTO.WorkLabel
{
    public class CreateLabelDto
    {
        [Key]
        [Required]
        public int roleId { get; set; }
        public string roleName { get; set; }
    }
}
