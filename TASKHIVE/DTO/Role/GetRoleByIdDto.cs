using TASKHIVE.Model;

namespace TASKHIVE.DTO.Role
{
    public class GetRoleByIdDto
    {
        public int roleId { get; set; }
        public UserRole userRole { get; set; }
    }
}
