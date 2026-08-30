


using SchoolProject.Core.Features.Authorization.Queries.Response;

namespace SchoolProject.Core.Mapping.Role
{
    public partial class RoleMapping
    {
        public void GetAllRoleMapping()
        {
            CreateMap<Data.Entities.Identity.Role, GetAllRoleResponse>();
                
        }
}
}
