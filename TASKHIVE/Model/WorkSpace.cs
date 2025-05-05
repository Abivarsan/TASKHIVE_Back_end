using System.ComponentModel.DataAnnotations;

namespace TASKHIVE.Model
{
    public class WorkSpace
    {
        [Key]
        public int workSpaceId { get; set; }
        public string workSpaceName { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Project> Projects { get; set; }
    }
}
