using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Student.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Student.Commands.Handlers
{
    public class StudentHandler : ResponseHandler
                                 ,IRequestHandler<AddStudentCommand, Response<string>>
                                 ,IRequestHandler<UpdateStudentCommand, Response<string>>
                                 ,IRequestHandler<DeleteStudentCommand, Response<bool>>
    {
        #region Fields
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        #endregion

        #region Constructor
        public StudentHandler(IStudentService studentService, IMapper mapper, IStringLocalizer<SharedResource> stringLocalizer) :base(stringLocalizer)
        {
            _studentService = studentService;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
          
        }
        #endregion

        #region Handles Functions 
        public async Task<Response<string>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            var student = _mapper.Map<Data.Entities.Student>(request);

            var studentStatus = await _studentService.AddAsync(student);

            if (studentStatus == "Exsit") return UnprocessableEntity<string>("Name is Exsit");

            else if(studentStatus == "Success") return Created("Add Successfully");

            else return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = _mapper.Map<Data.Entities.Student>(request);

            var studentStatus = await _studentService.UpdateAsync(student);

            if (studentStatus == "NotExist") return UnprocessableEntity<string>("Student is Not Exsit");

            else if (studentStatus == "Update") return Success("Update Successfully");

            else return BadRequest<string>();
        }

        public async Task<Response<bool>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var studentStatus = await _studentService.DeleteById(request.Id);

            if (studentStatus)  return Deleted<bool>();

            else return NotFound<bool>("Student not found");
        }
        #endregion
    }
}
