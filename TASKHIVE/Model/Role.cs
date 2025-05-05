using System.ComponentModel.DataAnnotations;

namespace TASKHIVE.Model
{
    public enum UserRole
    {
        AD,
        PM,
        SE,
        Dev,
        QA,
        UI
    }
    public class Role
    {
        [Key]
        [Required]
        public int roleId { get; set; }

        [Required]
        public UserRole userRole { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
