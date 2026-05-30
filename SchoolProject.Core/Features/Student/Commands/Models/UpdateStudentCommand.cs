using MediatR;
using SchoolProject.Core.Bases;
using System.ComponentModel.DataAnnotations;

namespace SchoolProject.Core.Features.Student.Commands.Models
{
    public class UpdateStudentCommand:IRequest<Response<string>>
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [MinLength(11)]
        public string Phone { get; set; }
        public int DepartmentID { get; set; }
    }
}
