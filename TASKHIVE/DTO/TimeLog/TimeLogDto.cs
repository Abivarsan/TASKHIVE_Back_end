using System.ComponentModel.DataAnnotations;
using TASKHIVE.Model;

namespace TASKHIVE.DTO.TimeLog
{
    public class TimeLogDto
    {
        [Key]
        [Required]
        public int timeLogId { get; set; }

        [Required]
        public DateTime logDate { get; set; }

        [Required]
        public int hoursWorked { get; set; }

        [Required]
        public int userId { get; set; }

        [Required]
        public int workId { get; set; }
    }
}
