namespace SchoolProject.Core.Features.Department.Queries.Response
{
    public partial class GetDepartmentByIdResponse
    {
        public class DepartmentSubjectsName
        {
            public DepartmentSubjectsName(string name)
            {
                Name = name;
            }
            public string Name { get; set; }
        }

    }

}

