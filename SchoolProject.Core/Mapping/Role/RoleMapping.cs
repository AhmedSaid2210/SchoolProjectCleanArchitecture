

using AutoMapper;

namespace SchoolProject.Core.Mapping.Role
{
    public partial class RoleMapping :Profile
    {
        public RoleMapping()
        {
            GetRoleByIdMapping();
            GetAllRoleMapping();
        }
    }
}
