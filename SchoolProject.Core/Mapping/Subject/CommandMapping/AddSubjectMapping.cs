

using SchoolProject.Core.Features.Subject.Queries.Models;

namespace SchoolProject.Core.Mapping.Subject
{
    public partial class SubjectProfile
    {
        public void AddSubjectMapping()
        {
            CreateMap<AddSubjectCommand, Data.Entities.Subjects>();
                
        }
    }
}
