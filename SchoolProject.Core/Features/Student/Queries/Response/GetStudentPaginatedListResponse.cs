

namespace SchoolProject.Core.Features.Student.Queries.Response
{
    public class GetStudentPaginatedListResponse
    {
        public int StudID { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? DepartmentName { get; set; }
        public GetStudentPaginatedListResponse(int id,string name,string address,string departmentName)
        {
            StudID = id;
            Name = name;
            Address = address;
            DepartmentName = departmentName;
        }
    }
}
