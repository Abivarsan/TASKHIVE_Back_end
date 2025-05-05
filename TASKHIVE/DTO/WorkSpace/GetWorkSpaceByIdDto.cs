using System.ComponentModel.DataAnnotations;
using TASKHIVE.Model;

namespace TASKHIVE.DTO.WorkSpace
{
    public class GetWorkSpaceByIdDto
    {
        public int workSpaceId { get; set; }
        public string workSpaceName { get; set; }
    }
}
