using System.ComponentModel.DataAnnotations;
using TASKHIVE.Model;

namespace TASKHIVE.DTO.Role
{
    public class GetAllRoleDto
    {
      
        public int roleId { get; set; }
        public UserRole userRole { get; set; }
    }
}
