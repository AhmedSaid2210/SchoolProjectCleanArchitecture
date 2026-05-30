
using SchoolProject.Core.Features.Department.Commands.Models;

namespace SchoolProject.Core.Mapping.Department
{
    public partial class DepartmentProfile
    {
        public void UpdateDepartmentMapping()
        {
            CreateMap<UpdateDepartmentCommand, Data.Entities.Department>()
                .ForMember(dest=>dest.Id , opt=>opt.MapFrom(src=>src.Id));
        }
    }
}
