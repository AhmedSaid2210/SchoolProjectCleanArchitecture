
using SchoolProject.Core.Features.Instructor.Commands.Models;

namespace SchoolProject.Core.Mapping.Instructor
{
    public partial class InstructorProfile
    {
        public void UpdateInstructorMapping()
        {
            CreateMap<UpdateInstructorCommand, Data.Entities.Instructor>()
                .ForMember(dest => dest.DID , opt=>opt.MapFrom(src=>src.DepartmentID));
        }
    }
}
