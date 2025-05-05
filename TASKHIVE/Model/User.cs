using System.ComponentModel.DataAnnotations;

namespace TASKHIVE.Model
{
   public class User
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

        public string ?profilePicture { get; set; }

        [Required]
        public int roleId { get; set; }
        public Role Role { get; set; }

        public int ?meetingId { get; set; }

        public Meeting meeting { get; set; }

        public int ?workSpaceId { get; set; }

        public WorkSpace workSpace { get; set; }


        public ICollection<TimeLog> TimeLogs { get; set; }
        public ICollection<Report> Reports { get; set; }
    }
}
