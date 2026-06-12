
using SchoolProject.Core.Features.User.Queries.Response;

namespace SchoolProject.Core.Mapping.User
{
    public partial class UserProfile
    {
        public void GetUserListMapping()
        {
            CreateMap<Data.Entities.Identity.User, GetUserPaginatedResponse>();
           
        }
    }
}
