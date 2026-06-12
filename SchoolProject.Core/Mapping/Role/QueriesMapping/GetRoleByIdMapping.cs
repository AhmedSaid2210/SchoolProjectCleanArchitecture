

using SchoolProject.Core.Features.Authorization.Queries.Response;

namespace SchoolProject.Core.Mapping.Role
{
    public partial class RoleMapping 
    {
        public void GetRoleByIdMapping()
        {
            CreateMap<Data.Entities.Identity.Role,GetRoleByIdResponse>();
        }
    }
}
