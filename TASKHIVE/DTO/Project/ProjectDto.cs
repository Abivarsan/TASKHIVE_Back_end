using System.ComponentModel.DataAnnotations;

namespace TASKHIVE.DTO.Project
{
    public class ProjectDto
    {
        [Key]
        [Required]
        public int projectId { get; set; }
        [Required]
        public string projectName { get; set; }
    }
}
