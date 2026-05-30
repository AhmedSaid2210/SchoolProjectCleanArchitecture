
namespace SchoolProject.Core.Features.Department.Queries.Response
{
    public class InstructorIdAndName
    {
        public InstructorIdAndName()
        {
            
        }
        public InstructorIdAndName(int id,string name)
        {
            Id = id;
            InstructorName = name;
        }
        public int Id { get; set; }
        public string InstructorName { get; set; }
    }
}
