

using MediatR;
using SchoolProject.Core.Bases;
using System.ComponentModel.DataAnnotations;

namespace SchoolProject.Core.Features.Subject.Queries.Models
{
    public class AddSubjectCommand:IRequest<Response<string>>
    {
        [Required]
        public string SubjectName { get; set; }
        public DateTime Period { get; set; }
    }
}
