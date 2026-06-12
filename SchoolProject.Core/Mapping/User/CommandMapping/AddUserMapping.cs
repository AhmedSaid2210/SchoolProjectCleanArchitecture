

using SchoolProject.Core.Features.User.Commands.Models;

namespace SchoolProject.Core.Mapping.User
{
    public partial class UserProfile
    {
        public void AddUserMapping()
        {
            CreateMap<AddUserCommand, Data.Entities.Identity.User>()
  
                ;
        }
    }
}
