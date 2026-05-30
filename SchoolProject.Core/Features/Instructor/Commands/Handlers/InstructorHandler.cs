

using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Instructor.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Instructor.Commands.Handlers
{
    public class InstructorHandler : ResponseHandler,
                                  IRequestHandler<AddInstructorCommand, Response<string>>,
                                  IRequestHandler<DeleteInstructorByIdCommand, Response<string>>,
                                  IRequestHandler<UpdateInstructorCommand, Response<string>>


    {
        private readonly IInstructorService _instructorService;
        private readonly IMapper _mapper;
        IStringLocalizer<SharedResource> _stringLocalizer;
        public InstructorHandler(IInstructorService instructorService, IMapper mapper,IStringLocalizer<SharedResource> stringLocalizer) : base(stringLocalizer)
        {
            _instructorService = instructorService;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response<string>> Handle(AddInstructorCommand request, CancellationToken cancellationToken)
        {
            var instructor = _mapper.Map<AddInstructorCommand,Data.Entities.Instructor>(request);

            var instructorStatus = await _instructorService.AddAsync(instructor);

            if (instructorStatus == "Success") return Created<string>("Add Successfully");

            else if (instructorStatus == "Exist") return UnprocessableEntity<string>("Name Is Exist");

            else return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(DeleteInstructorByIdCommand request, CancellationToken cancellationToken)
        {
            var instructor = await _instructorService.DeleteById(request.Id);

            if (instructor == "Success") return Deleted<string>("Deleted Successfully");

            else return BadRequest<string>(instructor);
        }

        public async Task<Response<string>> Handle(UpdateInstructorCommand request, CancellationToken cancellationToken)
        {
            var instructor = _mapper.Map<UpdateInstructorCommand,Data.Entities.Instructor>(request);

            var instructorStatus = await _instructorService.UpdateAsync(instructor);

            if (instructorStatus == "Success") return Success<string>("Update Successfully");

            else return BadRequest<string>(instructorStatus);
        }
    }
}
