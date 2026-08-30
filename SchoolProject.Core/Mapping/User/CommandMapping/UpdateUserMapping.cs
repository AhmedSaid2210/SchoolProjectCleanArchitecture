
using SchoolProject.Core.Features.User.Commands.Models;

namespace SchoolProject.Core.Mapping.User
{
    public partial class UserProfile
    {
        public void UpdateUserMapping()
        {
            CreateMap<UpdateUserCommand, Data.Entities.Identity.User>();
        }
    }
}
