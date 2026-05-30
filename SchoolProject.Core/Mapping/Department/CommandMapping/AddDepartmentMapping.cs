

using SchoolProject.Core.Features.Department.Commands.Models;
using SchoolProject.Core.Features.Student.Commands.Models;

namespace SchoolProject.Core.Mapping.Department
{
    public partial class DepartmentProfile
    {
        public void AddDepartmentMapping()
        {
            CreateMap<AddDepartmentCommand, Data.Entities.Department>();
                
        }
    }
}
