

using AutoMapper;

namespace SchoolProject.Core.Mapping.Department
{
    public partial class DepartmentProfile:Profile
    {
        public DepartmentProfile()
        {
            AddDepartmentMapping();
            UpdateDepartmentMapping();


            GetAllDepartmentMapping();
            GetDepartmentByIdMapping();
        }
    }
}
