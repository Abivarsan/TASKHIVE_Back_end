﻿using System.ComponentModel.DataAnnotations;

namespace TASKHIVE.DTO.WorkLabel
{
    public class UpdateWorkLabelDto
    {
        [Key]
        [Required]
        public int workLabelId { get; set; }

        [Required]
        public int workId { get; set; }
        public int labelId { get; set; }
    }
}
