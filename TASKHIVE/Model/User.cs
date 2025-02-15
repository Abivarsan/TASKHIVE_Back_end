﻿using System.ComponentModel.DataAnnotations;

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


        [Required]
        public int roleId { get; set; }
        public Role Role { get; set; }

        public ICollection<UserWork> UserWorks { get; set; }
        public ICollection<TimeLog> TimeLogs { get; set; }
        public ICollection<Report> Reports { get; set; }
        public ICollection<UserMeeting> UserMeetings { get; set; }
    }
}
