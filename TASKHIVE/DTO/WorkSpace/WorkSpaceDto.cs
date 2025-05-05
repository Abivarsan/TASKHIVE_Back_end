using System.ComponentModel.DataAnnotations;
using TASKHIVE.Model;

namespace TASKHIVE.DTO.WorkSpace
{
    public class WorkSpaceDto
    {
        [Key]
        public int workSpaceId { get; set; }
        public string workSpaceName { get; set; }
    }
}
